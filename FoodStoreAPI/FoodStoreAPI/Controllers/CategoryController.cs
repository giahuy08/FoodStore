using FoodStoreAPI.Commons.Paging;
using FoodStoreAPI.Commons.Parameter;
using FoodStoreAPI.Controller;
using FoodStoreAPI.Entities.Identity;
using FoodStoreAPI.Features.Commands;
using FoodStoreAPI.Features.Queries;
using FoodStoreAPI.Features.Resources.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FoodStoreAPI.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediatr;
        private readonly UserManager<User> _userManager;
        public CategoryController(UserManager<User> userManager, IMediator mediatr)
        {
            _mediatr = mediatr;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]CategoryQueryStringParameters query)
        {
            var result = await _mediatr.Send(new GetAllCategoryQuery() { });
            var paging = PagedList<CategoryRequest>.ToPagedList(result.AsQueryable(), query.PageNumber, query.PageSize); ;
            return Ok(paging);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediatr.Send(new CreateCategoryCommand() { createCategory = request });
            if (result.Succeeded) return StatusCode(200);
            return BadRequest(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediatr.Send(new UpdateCategoryCommand() { UpdateCategory = request, Id = id });
            if (result.Succeeded) return Ok(result);
            return BadRequest(result);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> DeleteSoftCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediatr.Send(new DeleteSoftCategoryCommand() { Id = id });
            if (result.Succeeded) return Ok(result);
            return BadRequest(result);
        }
    }
}
