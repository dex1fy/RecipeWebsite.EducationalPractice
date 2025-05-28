using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteBackend.Models.DTOs.Registration;
using RecipeWebsiteBackend.Models.Entities;
using RecipeWebsiteBackend.Services;
using System.Security.Claims;
using Supabase.Gotrue;
using Supabase.Postgrest;
using RecipeWebsiteBackend.Models.DTOs.Authentication;

namespace RecipeWebsiteBackend.Controllers
{
    // контроллер аутентификации. проверяет токен доступа
    // в качестве ответа возвращает айди вошедшего пользователя
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// получение сервиса супабейза (паттерн DI)
        /// </summary>
        private readonly SupabaseService _supabaseService;
        public AuthenticationController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        /// <summary>
        /// авторизация пользователя
        /// следует вернуть пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("login")]
        public async Task<IActionResult> LoginUserAsync()
        {
            // получение айди пользователя через токен доступа
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // проверка гуид
            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("invalid uuid format");

            // получение клиента
            var supabaseClient = await _supabaseService.InitSupabase();

            // формирование ответа (передачи модели)
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
            });
        }

        /// <summary>
        /// регистрация пользователя
        /// тут будут всякие проверки валидации и хеширование паролей
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // TODO: реализовать хеширование
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request)
        {
            var supabaseClient = await _supabaseService.InitSupabase();

            // проверяет, соответствие модели, указанным правилам в DTO (посмотрите модель RegisterRequest)
            if(!ModelState.IsValid) 
                return BadRequest(ModelState); // если данные невалидны (почта, пароль), то возвращаем 400

            // проверка соответствия пароля
            if (request.Password != request.ConfirmPassword)
                return BadRequest(new { message = "пароли не совпадают" });

            // регистрация
            var register = await supabaseClient.Auth.SignUp(
                email: request.Email, 
                password: request.Password
            );
            if (register != null)
            {
                // создается модель пользователя
                var userModel = new UserModel
                {
                    Id = Guid.Parse(register.User.Id),
                    Name = request.Name,
                    UserName = request.UserName,
                };

                // заполянем таблицу пользователей (не аутентификация)
                var user = await supabaseClient.From<UserModel>().Insert(userModel);

                return Ok(new
                {
                    message = $"successful user registration {userModel.UserName}"
                });
            }
            return BadRequest("user registration error");
        }
    }
}
