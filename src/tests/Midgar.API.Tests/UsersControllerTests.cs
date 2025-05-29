using Moq;
using Midgar.API.Controllers;
using Midgar.Persistence.Interfaces;
using Midgar.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Midgar.API.Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly UsersController _usersController;

        public UsersControllerTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _usersController = new UsersController(_mockUserRepo.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkObjectResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1, Name = "Test User", Email = "test@example.com", Phone = "8599999999"} };
            _mockUserRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _usersController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<User>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Test User", returnValue[0].Name);
        }

        [Fact]
        public async Task GetById_UserExists_ReturnsOkObjectResult_WithUser()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Name = "Test User", Email = "test@example.com", Phone = "12345" };
            _mockUserRepo.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _usersController.Get(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(userId, returnValue.Id);
        }

        [Fact]
        public async Task GetById_UserDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = 1;
            _mockUserRepo.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _usersController.Get(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ValidUser_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newUser = new User { Name = "New User", Email = "new@example.com", Phone = "555-1234" };
            _mockUserRepo.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                         .Callback<User>(u => u.Id = 1)
                         .Returns(Task.CompletedTask);

            // Act
            var result = await _usersController.Post(newUser);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_usersController.Get), createdAtActionResult.ActionName);
            Assert.NotNull(createdAtActionResult.RouteValues);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(newUser, createdAtActionResult.Value);
        }

        [Fact]
        public async Task Post_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var newUser = new User { Name = "" }; 
            _usersController.ModelState.AddModelError("Name", "The name is required."); 

            // Act
            var result = await _usersController.Post(newUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<SerializableError>(badRequestResult.Value); 
        }
    }
}