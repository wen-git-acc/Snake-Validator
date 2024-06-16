using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Snake.Validator.Service.Controllers;
using Snake.Validator.Service.Payload;
using Snake.Validator.Service.Response;
using Snake.Validator.Service.Services;
using Xunit;

namespace Snake.Validator.Service.Tests.Controllers;

public class SnakeGameControllerTests
{
    public SnakeGameController GetController()
    {
        var mockLogger = new Mock<ILogger<SnakeGameController>>();

        var mockNewGame = new Mock<INewGame>();
        mockNewGame
            .Setup(game => game.StartGame(
                It.IsAny<int>(),
                It.IsAny<int>()))
            .Returns(new StateConfig());

        var mockValidator = new Mock<ISnakeValidator>();
        mockValidator
            .Setup(validator => validator.VerifyData(It.IsAny<ValidateConfig>()))
            .Returns(new StateConfig());

        return new SnakeGameController(
            mockLogger.Object,
            mockNewGame.Object,
            mockValidator.Object);
    }

    [Fact]
    public async void ValidateStateTest_ShouldReturnActionResult()
    {
        // Arrange
        var controller = GetController(); // Inject the mocked service

        // Act
        var actionResult = await controller.ValidateState(new ValidateConfig());

        // Assert
        Assert.IsType<ActionResult<StateConfig>>(actionResult); 
    } 
    
    [Fact]
    public async void GetNewGameTest_ShouldReturnActionResult()
    {
        // Arrange
        var controller = GetController(); // Inject the mocked service

        // Act
        var actionResult = await controller.GetNewGame(10,10);

        // Assert
        Assert.IsType<ActionResult<StateConfig>>(actionResult); 
    }
}