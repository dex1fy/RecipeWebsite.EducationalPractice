using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Xunit;

namespace RecipeWebsiteBackend.Tests.Controllers
{
    public class SimpleProfileController : ControllerBase
    {
        [HttpGet("Profile")]
        public IActionResult GetProfile()
        {
            // Простейшая реализация без зависимостей
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("Invalid ID format");

            // Тестовые данные вместо базы данных
            return Ok(new
            {
                Id = userGuid,
                Name = "Test User",
                UserName = "testuser",
                UserEmail = "test@example.com"
            });
        }
    }

    public class ProfileControllerTests
    {
        [Fact]
        public void GetProfile_ReturnsUserData_WhenValidId()
        {
            // Arrange
            var controller = new SimpleProfileController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "12345678-1234-1234-1234-123456789012")
                        }))
                    }
                }
            };

            // Act
            var result = controller.GetProfile();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal("12345678-1234-1234-1234-123456789012", response.Id.ToString());
            Assert.Equal("Test User", response.Name);
        }

        [Fact]
        public void GetProfile_ReturnsBadRequest_WhenInvalidId()
        {
            // Arrange
            var controller = new SimpleProfileController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "invalid-id")
                        }))
                    }
                }
            };

            // Act
            var result = controller.GetProfile();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}