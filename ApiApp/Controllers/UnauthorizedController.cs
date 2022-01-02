using Microsoft.AspNetCore.Mvc;
namespace ApiApp.Controllers
{
    [Route("api")]
    public class UnauthorizedController : Controller
    {
        [Route("visibility")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { isServerVisible = true });
        }
    }
}
