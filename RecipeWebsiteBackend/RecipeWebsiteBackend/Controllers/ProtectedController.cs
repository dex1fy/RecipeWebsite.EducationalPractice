using Microsoft.AspNetCore.Mvc;

namespace RecipeWebsiteBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "access" });
        }
    }
}
