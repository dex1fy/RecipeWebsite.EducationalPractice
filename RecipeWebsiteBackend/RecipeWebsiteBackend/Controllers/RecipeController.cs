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

   
    // Новый метод для получения конкретного рецепта
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById(string id)
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

            var dishProduct = await supabaseClient
                .From<DishProductModel>()
                .Where(x => x.DishId == recipeResponse.Id)
                .Get();

            var productIds = dishProduct.Models.Select(dp => dp.ProductId).ToList();

             // 2.Получаем связанные ингредиенты
                var allProductsResponse = await supabaseClient
                    .From<ProductModel>()
                    .Get();

                var ingredientsList = allProductsResponse.Models
                    .Where(p => productIds.Contains(p.Id))
                    .ToList();

        var ingredientDtos = ingredientsList.Select(p => new IngredientDto
        {
            ProductId = p.Id,             // Или p.ProductId, если так называется
            ProductName = p.name          // Или p.Title, или другое имя свойства
        }).ToList();
        // 3. Формируем DTO с полной информацией
        var recipeDetails = new RecipeDetailsDto
            {
                Name = recipeResponse.Name,
                Steps = recipeResponse.Steps,
                Ingredients = ingredientDtos,
                CookingTime = recipeResponse.CookingTime,
                Calories = recipeResponse.Calories,
                Fats = recipeResponse.Fats,
                Proteins = recipeResponse.Squirrels,
                ShortDescription = recipeResponse.ShortDescription,
                Carbohydrates = recipeResponse.Carbohydrates,
        };

            return Ok(recipeDetails);
        }
        
    }
