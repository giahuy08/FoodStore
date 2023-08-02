using AutoMapper;
using FoodStoreAPI.Commons.Constants;
using FoodStoreAPI.Commons.Enums;
using FoodStoreAPI.Commons.Helpers;
using FoodStoreAPI.Controller;
using FoodStoreAPI.Entities.Identity;
using FoodStoreAPI.Resources.Responses;
using FoodStoreAPI.Services.Interfaces;
using FoodStoreAPI.ViewModel.Mail;
using FoodStoreAPI.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;

namespace FoodStoreAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISendMailService _emailService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager,
                              IMapper mapper, RoleManager<IdentityRole> roleManager, AuthenticatorTokenProvider<User> authenticatorTokenProvider,
                              ISendMailService emailService, IWebHostEnvironment env, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _emailService = emailService;
            _env = env;
            _configuration = configuration;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user == null)
            {
                return BadRequest(Constants.UNAUTHORIZE);
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, userModel.Password);
            if (!checkPassword)
            {
                return BadRequest(Constants.USER_OR_PASSWORD_ERROR);
            }

            var tokenGenerate = TokenHelper.GenerateToken(_configuration, user);
            return Ok(new { token = tokenGenerate });
        }
        
        
        /// <summary>
        /// Register Customer
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userResponse = await _userManager.FindByEmailAsync(userModel.Email);
            if (userResponse != null)
            {
                if (userResponse.EmailConfirmed)
                {
                    return Unauthorized(Constants.USER_EXISTED);
                }
                else
                {
                    await _userManager.DeleteAsync(userResponse);
                }
            }

            var user = _mapper.Map<User>(userModel);
            var response = await _userManager.CreateAsync(user, userModel.Password);
            if (!response.Succeeded)
            {
                return BadRequest(Constants.CANNOT_CREATE);
            }

            var roleName = RoleEnum.Customer.ToString();
            var checkExistRole = await _roleManager.RoleExistsAsync(roleName);
            if (!checkExistRole)
            {
                var role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmLink = String.Empty;

            await _userManager.AddToRoleAsync(user, roleName);
            if (_env.IsDevelopment())
            {

                confirmLink = Url.Action("ConfirmEmail", "User", new { email = user.Email, token = token }, protocol: HttpContext.Request.Scheme);
            }
            else
            {
                confirmLink = Url.Action
                   (
                       "ConfirmEmail", "User",
                       new { email = user.Email, token },
                       protocol: "https",
                       host: ""
                   );
            }

            if (confirmLink != null)
            {
                var email = new SendMailViewModel()
                {
                    ToEmail = user.Email,
                    UserName = user.Email,
                    ComfirmEmailLink = confirmLink
                };
                await _emailService.SendMailAsync(email);
            }
            return Ok(Constants.USER_CREATE_SUCCESS);
        }

        /// <summary>
        /// Confirm Email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token, string type)
        {
            ConfirmEmailType confirmType;
            if (!Enum.TryParse(type, out confirmType))
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(Constants.USER_NOT_FOUND);
            }

            switch ((confirmType))
            {
                case ConfirmEmailType.ConfirmEmailRegister:
                    var response = await _userManager.ConfirmEmailAsync(user, token);
                    if (response.Succeeded)
                    {
                        return RedirectLink("http://localhost:3000/", "");
                    }
                    return BadRequest(Constants.CONFIRM_EMAIL_ERROR);

                case ConfirmEmailType.ConfirmChangePassword:
                    var linkLocal = String.Format("http://localhost:3000/forgotPassword?token={0}&email={1}", token, email);
                    return RedirectLink(linkLocal, "");
                default:
                    break;
            }
            return Ok(Constants.CONFIRM_EMAIL_SUCCESS);
        }

        private RedirectResult RedirectLink(String linkLocal, String linkDeploy)
        {
            string redirectUrl = String.Empty;
            if (_env.IsDevelopment())
            {
                redirectUrl = linkLocal;
            }
            else
            {
                redirectUrl = linkDeploy;
            }
            return Redirect(redirectUrl);
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userContext = HttpContext.User;
            var user = await _userManager.GetUserAsync(userContext);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return Unauthorized(Constants.UNAUTHORIZE);
            }
            var response = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!response.Succeeded)
            {
                return BadRequest(Constants.UPDATE_PASSWORD_FAIL);
            }
            return Ok(Constants.UPDATE_PASSWORD_SUCCESS);
        }

        [HttpPatch("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(Constants.USER_NOT_FOUND);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmLink = String.Empty;
            if (_env.IsDevelopment())
            {

                confirmLink = Url.Action("ConfirmEmail", "User", new { email = user.Email, token = token, type = ConfirmEmailType.ConfirmChangePassword }, protocol: HttpContext.Request.Scheme);
            }
            else
            {
                confirmLink = Url.Action
                   (
                "ConfirmEmail", "User",
                       new { email = user.Email, token },
                       protocol: "https",
                       host: ""
                   );
            }

            if (confirmLink != null)
            {
                var emailModel = new SendMailViewModel()
                {
                    ToEmail = user.Email,
                    UserName = user.Email,
                    ComfirmEmailLink = confirmLink
                };
                await _emailService.SendMailAsync(emailModel);
            }
            return Ok(Constants.SEND_EMAIL_SUCCESS);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(Constants.USER_NOT_FOUND);
            }

            var resetPassword = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassword.Succeeded)
            {
                return BadRequest(Constants.UPDATE_PASSWORD_FAIL);
            }

            return Ok(Constants.UPDATE_PASSWORD_SUCCESS);
        }

    }
}