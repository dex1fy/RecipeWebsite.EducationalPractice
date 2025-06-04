using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteBackend.Models.Entities;
using RecipeWebsiteBackend.Services;

namespace RecipeWebsiteBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;

        public IngredientsController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCuisine()
        {
            var supabaseClient = await _supabaseService.InitSupabase();

            // формирование ответа (передачи модели)
            var response = await supabaseClient
                .From<ProductModel>().Get();

            var Data = response.Models;

            return Ok(response.Content);

        }
    }
}
