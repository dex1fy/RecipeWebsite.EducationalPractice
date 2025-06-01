using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteBackend.Models.Entities;
using RecipeWebsiteBackend.Services;

namespace RecipeWebsiteBackend.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;

        public CategoriesController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var supabaseClient = await _supabaseService.InitSupabase();

            // формирование ответа (передачи модели)
            var response = await supabaseClient
                .From<CategoryDish>().Get();

            var Data = response.Models;
             
            return Ok(response.Content);
          // возвращает [{ id: ..., name: ... }, ...]
        }
    }
}
