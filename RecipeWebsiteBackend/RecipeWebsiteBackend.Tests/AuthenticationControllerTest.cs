using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RecipeWebsiteBackend.Controllers;
using RecipeWebsiteBackend.Models.DTOs.Authentication;
using RecipeWebsiteBackend.Models.DTOs.Registration;
using RecipeWebsiteBackend.Models.Entities;
using RecipeWebsiteBackend.Services;
using Supabase;
using System.Security.Claims;
using Xunit;

namespace RecipeWebsiteBackend.Tests.Controllers
{
    public class AuthenticationTests
    {
        // Заглушка для SupabaseService
        private class TestSupabaseService : SupabaseService
        {
            public UserModel? UserToReturn { get; set; }
            public bool UserExists { get; set; }
            public bool SignUpSuccess { get; set; } = true;

            public TestSupabaseService()
                : base(new ConfigurationBuilder().AddInMemoryCollection().Build())
            {
            }

            public virtual Task<UserModel?> GetUserAsync(Guid userId)
            {
                return Task.FromResult(UserToReturn);
            }

            public virtual Task<bool> CheckUserExistsAsync(string username, string email)
            {
                return Task.FromResult(UserExists);
            }

            public virtual Task<bool> RegisterUserAsync(UserModel user, string password)
            {
                return Task.FromResult(SignUpSuccess);
            }
        }

        public class AuthenticationControllerTests
        {
            private AuthenticationController CreateControllerWithUser(string userId, SupabaseService service)
            {
                var controller = new AuthenticationController(service);
                var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
                var identity = new ClaimsIdentity(claims);
                controller.ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
                };
                return controller;
            }


            //проверяет поведение метода LoginUserAsync() в случае,
            //если идентификатор пользователя в JWT-токене некорректный и не может быть преобразован в Guid.

            [Fact]
            public async Task LoginUserAsync_ReturnsBadRequest_WhenInvalidGuid()
            {
                // Arrange
                var controller = CreateControllerWithUser("not-a-guid", new TestSupabaseService());

                // Act
                var result = await controller.LoginUserAsync();

                // Assert
                Assert.IsType<BadRequestObjectResult>(result);
            }

          
        }

    }
}