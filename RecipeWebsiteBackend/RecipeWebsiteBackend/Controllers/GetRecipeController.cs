using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteBackend.Models.DTOs.Recipe;
using RecipeWebsiteBackend.Models.Entities;
using RecipeWebsiteBackend.Services;
using Supabase.Interfaces;
using static Supabase.Postgrest.Constants;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly SupabaseService _supabaseService;

    public RecipesController(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecipes()
    {
        try
        {
            var supabaseClient = await _supabaseService.InitSupabase();

            // формирование ответа (передачи модели)
            var response = await supabaseClient
                .From<Recipe>()
                .Select("id, name, cooking_time")
                .Order(x => x.Name, Ordering.Ascending)
                .Get();

            var recipes = response.Models.Select(r => new RecipeDto
            {
                Id = r.Id,
                Name = r.Name,
                CookingTime = r.CookingTime,
            }).ToList();

            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Произошла ошибка на сервере");
        }
    }

}