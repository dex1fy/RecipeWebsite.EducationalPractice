using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteBackend.Models.DTOs.Recipe;
using RecipeWebsiteBackend.Models.Entities;
using RecipeWebsiteBackend.Services;
using Supabase.Interfaces;
using static Supabase.Postgrest.Constants;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly SupabaseService _supabaseService;

    public RecipeController(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    // Существующий метод получения всех рецептов
    [HttpGet]
    public async Task<IActionResult> GetAllRecipes()
    {
        try
        {
            var supabaseClient = await _supabaseService.InitSupabase();

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

    // Новый метод для получения конкретного рецепта
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById(Guid id)
    {
        try
        {
            var supabaseClient = await _supabaseService.InitSupabase();

            // 1. Получаем основной рецепт
            var recipeResponse = await supabaseClient
                .From<Recipe>()
                .Select("*")
                .Filter("id", Operator.Equals, id)
                .Single();

            if (recipeResponse == null)
                return NotFound("Рецепт не найден");

            // 2. Получаем связанные ингредиенты
            var ingredientsResponse = await supabaseClient
                .From<DishProductModel>()
                .Select("product_id, product:product_id(name)")
                .Filter("dish_id", Operator.Equals, id)
                .Get();

            // 3. Формируем DTO с полной информацией
            var recipeDetails = new RecipeRequest
            {
                //Id = .Id,
                //Name = recipeResponse.Model.Name,
                //CookingTime = recipeResponse.Model.CookingTime,
                //Steps = recipeResponse.Model.Steps,
                //Image = recipeResponse.Model.Image,
                //ShortDescription = recipeResponse.Model.ShortDescription,
                //Proteins = recipeResponse.Model.Squirrels,
                //Fats = recipeResponse.Model.Fats,
                //Carbohydrates = recipeResponse.Model.Carbohydrates,
                //Calories = recipeResponse.Model.Calories,
                //Ingredients = ingredientsResponse.Models.Select(i => new IngredientDto
                //{
                //    ProductId = i.ProductId,
                //    ProductName = i.Product?.name,
                //}).ToList()
            };

            return Ok(recipeDetails);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при получении рецепта: {ex.Message}");
        }
    }
}