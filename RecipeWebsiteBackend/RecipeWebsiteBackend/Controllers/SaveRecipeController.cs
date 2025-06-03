// Controllers/RecipesController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using RecipeWebsiteBackend.Models.DTOs.Recipe;
using RecipeWebsiteBackend.Models.Entities;
using RecipeWebsiteBackend.Services;
using System.Diagnostics;

/// <summary>
/// КОНТРОЛЛЕР ДЛЯ РАБОТЫ С РЕЦЕПТАМИ
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SaveRecipeController : ControllerBase
{
    private readonly SupabaseService _supabaseService; // объявление срвиса супабейза

    public SaveRecipeController(SupabaseService supabaseService) // конструктор класса
    {
        _supabaseService = supabaseService;
    }

    /// <summary>
    /// TODO: ПУСТОЙ КОНТРОЛЛЕР ЧИСТО НА ВСТАВКУ БЕЗ ВСЕГО НАДО ДОРАБОТАТЬ! 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize] // проверка авторизации
    [HttpPost("createrecipe")] // теперь название /api/SaveRecipe/createrecipe
    public async Task<IActionResult> CreateRecipe([FromBody] RecipeRequest request)
    {
        var supabaseClient = await _supabaseService.InitSupabase(); // получаем коннект с супой
        var recipeModdel = new Recipe // создаем модель рецепта и запихиваем в нее данные из тела запроса (request)
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Steps = request.Steps,
            CatKey = request.CatKey,
            CatCuisineKey = request.CatCuisineKey,
            CatMenuKey = request.CatMenuKey,
            ShortDescription = request.ShortDescription,
            Squirrels = request.Squirrels,
            Fats = request.Fats,
            Carbohydrates = request.Carbohydrates,
        };

        var recipe = await supabaseClient.From<Recipe>().Insert(recipeModdel); // в таблицу dish, которая указана в модели Recipe вставляем новые поля

        return Ok(); // все норм
    }
}