using System.Security.Claims;

public class ProfileControllerTests
{
    private readonly Mock<SupabaseService> _mockSupabaseService;
    private readonly ProfileController _controller;

    public ProfileControllerTests()
    {
        _mockSupabaseService = new Mock<SupabaseService>();
        _controller = new ProfileController(_mockSupabaseService.Object);
    }

    private void SetUser(Guid userId)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task LoginUserProfileAsync_ReturnsOk_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        SetUser(userId);

        var mockClient = new Mock<Client>();

        var expectedUser = new UserModel
        {
            Id = userId,
            Name = "Test User",
            UserName = "testuser",
            UserEmail = "test@example.com"
        };

        var queryable = new[] { expectedUser }.AsQueryable();

        var mockTable = new Mock<Table<UserModel>>();
        mockTable.Setup(x => x.Where(Any<Func<UserModel, bool>>()))
                 .Returns(mockTable.Object);
        mockTable.Setup(x => x.Single())
                 .ReturnsAsync(expectedUser);

        mockClient.Setup(x => x.From<UserModel>()).Returns(mockTable.Object);

        _mockSupabaseService.Setup(x => x.InitSupabase()).ReturnsAsync(mockClient.Object);

        // Act
        var result = await _controller.LoginUserProfileAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<UserResponse>(okResult.Value);
        Assert.Equal(expectedUser.Name, response.Name);
    }

    [Fact]
    public async Task LoginUserProfileAsync_ReturnsBadRequest_WhenInvalidUserId()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "invalid-guid")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var result = await _controller.LoginUserProfileAsync();

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("invalid uuid format", badRequest.Value);
    }

    [Fact]
    public async Task LoginUserProfileAsync_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        SetUser(userId);

        var mockClient = new Mock<Client>();
        var mockTable = new Mock<Table<UserModel>>();
        mockTable.Setup(x => x.Where(Any<Func<UserModel, bool>>()))
                 .Returns(mockTable.Object);
        mockTable.Setup(x => x.Single()).ReturnsAsync((UserModel)null);

        mockClient.Setup(x => x.From<UserModel>()).Returns(mockTable.Object);
        _mockSupabaseService.Setup(x => x.InitSupabase()).ReturnsAsync(mockClient.Object);

        // Act
        var result = await _controller.LoginUserProfileAsync();

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("User not found", notFound.Value);
    }
}