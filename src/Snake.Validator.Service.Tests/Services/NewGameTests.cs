using Microsoft.AspNetCore.Mvc;
using Moq;
using Snake.Validator.Service.Response;
using Snake.Validator.Service.Services;
using Xunit;

namespace Snake.Validator.Service.Tests.Services;

public class NewGameTests
{
    public INewGame GetService()
    {
        var mockFruitPos = new FruitConfig() { X = 10, Y = 10 };
        var mockGeneralHelper = new Mock<IGeneralHelper>();
        mockGeneralHelper
            .Setup(mock =>
                mock.GenerateFruitPosition(
                    It.IsAny<int>()
                    , It.IsAny<int>()
                    , It.IsAny<Random>()))
            .Returns(mockFruitPos);
        return new NewGame(mockGeneralHelper.Object);
    }

    [Theory]
    [InlineData(10,10)]
    [InlineData(20,120)]
    public void StartGameTests_WithData_ReturnSuccessResponse(int width, int height)
    {
        //Arrange
        var service = GetService();
        var expectedWidth = width;
        var expectedHeight = height;
        var expectedScore = 0;
        var expectedSnakeVelX = 1;
        var expectedSnakeVelY = 0;
        var expectedSnakeX = 0;
        var expectedSnakeY = 0;
        var expectedStatusCode = 200;

        //Act
        var actionResult = service.StartGame(width, height);
        var okResult = (OkObjectResult)actionResult.Result;
        var newState = (StateConfig) okResult.Value;

        //Assert
        Assert.IsType<OkObjectResult>(okResult);
        Assert.IsType<StateConfig>(newState);
        Assert.Equal(expectedStatusCode, okResult.StatusCode);
        Assert.Equal(expectedWidth, newState.Width);
        Assert.Equal(expectedHeight, newState.Height);
        Assert.Equal(expectedScore, newState.Score);
        Assert.Equal(expectedSnakeVelX,newState.Snake.VelX);
        Assert.Equal(expectedSnakeVelY, newState.Snake.VelY);
        Assert.Equal(expectedSnakeX,newState.Snake.X);
        Assert.Equal(expectedSnakeY,newState.Snake.Y);
    }

    [Theory]
    [InlineData(4, 5,400)]
    [InlineData(5, 3,400)]
    [InlineData(3, 3,400)]
    public void StartGameTests_WithInvalidSizeData_ReturnCorrectResponse(int width, int height, int expectedStatusCode)
    {
        //Arrange
        var service = GetService();

        //Act
        var actionResult = service.StartGame(width, height);
        var result = (ObjectResult)actionResult.Result;

        //Assert
        Assert.Equal(expectedStatusCode, result.StatusCode);
    }
}