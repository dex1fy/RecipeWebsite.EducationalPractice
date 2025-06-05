using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsiteBackend.Tests.Controllers;
using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecipeWebsiteBackend.Tests
{
    public class SimpleCategoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCuisine()
        {
           
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("Invalid ID format");

            return Ok(new
            {
                Id = userGuid,
                Name = "Test category",
            });
        }


        [HttpGet]
        public IActionResult GetCategories()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("Invalid ID format");


            return Ok(new
            {
                Id = userGuid,
                Name = "Test category",
            });
        }


        [HttpGet]
        public IActionResult GetCategoriesMenu()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("Invalid ID format");


            return Ok(new
            {
                Id = userGuid,
                Name = "Test category",
            });
        }



        [HttpGet]
        public IActionResult GetRecipe()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("Invalid ID format");


            return Ok(new
            {
                Id = userGuid,
                Name = "Test category",
                CookingTime = "10"
            }); 
        }


        [HttpGet]
        public IActionResult Recipe()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("Invalid ID format");


            return Ok(new
            {
                Id = userGuid,
                Name = "Test category",
                CookingTime = "10"
            });
        }

        [HttpGet]
        public IActionResult SaveRecipe()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var userGuid))
                return BadRequest("Invalid ID format");


            return Ok(new
            {
                Id = userGuid,
                Name = "Test category",
                CookingTime = "10"
            });
        }
    }




    public class CategoryTests
    {
        [Fact]
        public void GetCuisine_ReturnsData_WhenValidId()
        {
            // Arrange
            var controller = new SimpleCategoryController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "1da5b7d6-4409-447f-81c2-c1abcce5090c")
                        }))
                    }
                }
            };

            // Act
            var result = controller.GetCuisine();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal("1da5b7d6-4409-447f-81c2-c1abcce5090c", response.Id.ToString());
            Assert.Equal("Test category", response.Name);
        }

        [Fact]
        public void GetCategory_ReturnsData_WhenValidId()
        {
            // Arrange
            var controller = new SimpleCategoryController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "1da5b7d6-4409-447f-81c2-c1abcce5090c")
                        }))
                    }
                }
            };

            // Act
            var result = controller.GetCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal("1da5b7d6-4409-447f-81c2-c1abcce5090c", response.Id.ToString());
            Assert.Equal("Test category", response.Name);

        }



        [Fact]
        public void GetCategoryMenu_ReturnsData_WhenValidId()
        {
            // Arrange
            var controller = new SimpleCategoryController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "1da5b7d6-4409-447f-81c2-c1abcce5090c")
                        }))
                    }
                }
            };

            // Act
            var result = controller.GetCategoriesMenu();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal("1da5b7d6-4409-447f-81c2-c1abcce5090c", response.Id.ToString());
            Assert.Equal("Test category", response.Name);
        }

        [Fact]
        public void GetRecipe_ReturnsData_WhenValidId()
        {
            // Arrange
            var controller = new SimpleCategoryController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "1da5b7d6-4409-447f-81c2-c1abcce5090c")
                        }))
                    }
                }
            };

            // Act
            var result = controller.GetRecipe();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal("1da5b7d6-4409-447f-81c2-c1abcce5090c", response.Id.ToString());
            Assert.Equal("Test category", response.Name);
            Assert.Equal("10", response.CookingTime);
        }

        [Fact]
        public void Recipe_ReturnsData_WhenValidId()
        {
            // Arrange
            var controller = new SimpleCategoryController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "1da5b7d6-4409-447f-81c2-c1abcce5090c")
                        }))
                    }
                }
            };

            // Act
            var result = controller.Recipe();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal("1da5b7d6-4409-447f-81c2-c1abcce5090c", response.Id.ToString());
            Assert.Equal("Test category", response.Name);
            Assert.Equal("10", response.CookingTime);
        }

        [Fact]
        public void SaveRecipe_ReturnsData_WhenValidId()
        {
            // Arrange
            var controller = new SimpleCategoryController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, "1da5b7d6-4409-447f-81c2-c1abcce5090c")
                        }))
                    }
                }
            };

            // Act
            var result = controller.SaveRecipe();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal("1da5b7d6-4409-447f-81c2-c1abcce5090c", response.Id.ToString());
            Assert.Equal("Test category", response.Name);
            Assert.Equal("10", response.CookingTime);
        }


    }
}
