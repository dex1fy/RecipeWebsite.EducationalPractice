// Controllers/RecipesController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteBackend.Models.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SaveRecipeController : ControllerBase
{
    private readonly Supabase.Client _supabase;

    public SaveRecipeController(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
    {
        return Ok(recipe);

    }
}