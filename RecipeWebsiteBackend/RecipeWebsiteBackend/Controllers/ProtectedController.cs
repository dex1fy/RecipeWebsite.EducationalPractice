using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RecipeWebsiteBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var allClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            return Ok(new
            {
                Message = "Доступ разрешен",
                UserId = userId,
                Email = userEmail,
                AllClaims = allClaims
            });
        }
    }
}
