using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.OData;

namespace FoodStoreAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [EnableQuery]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
