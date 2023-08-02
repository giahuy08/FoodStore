using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodStoreAPI.Controller
{
    public class ProductController : BaseController
    {
       
        public ProductController()
        {

        }

       
        [Authorize]
        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(new { ProductId = 1 });
        }

        
    }
}
