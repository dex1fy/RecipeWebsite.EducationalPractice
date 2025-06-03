using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteBackend.Models.Entities;
using RecipeWebsiteBackend.Services;
using System.Security.Claims;
using Supabase.Gotrue;
using Supabase.Postgrest;
using RecipeWebsiteBackend.Models.DTOs.Authentication;

namespace RecipeWebsiteBackend.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;
        public ProfileController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> LoginUserProfileAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("invalid uuid format");

            var supabaseClient = await _supabaseService.InitSupabase();

            var response = await supabaseClient
                .From<UserModel>()
                .Where(x => x.Id == userGuid)
                .Single();

            if (response == null)
            {
                return NotFound("User not found");
            }

            return Ok(new UserResponse
            {
                Id = userGuid,
                Name = response.Name,
                UserName = response.UserName,
                UserEmail = response.UserEmail,
            });
        }
    }
}