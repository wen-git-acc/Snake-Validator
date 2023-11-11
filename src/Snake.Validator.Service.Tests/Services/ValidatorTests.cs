using Microsoft.AspNetCore.Mvc;
using Moq;
using Snake.Validator.Service.Messages;
using Snake.Validator.Service.Payload;
using Snake.Validator.Service.Response;
using Snake.Validator.Service.Services;
using Xunit;

namespace Snake.Validator.Service.Tests.Services;

public class ValidatorTests
{
    public ISnakeValidator GetService()
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
        return new SnakeValidator(mockGeneralHelper.Object);
    }

    [Fact]
    public void VerifyDataTests_WithValidData_ReturnSuccessMessage()
    {
        //Arrange
        var service = GetService();
        var validState = GetValidGameState();
        var expectedStatusCode = 200;
        var expectedScore = validState.Score + 1;
        var expectedGameId = validState.GameID;
        var expectedWidth = validState.Width;
        var expectedHeight = validState.Height;
        var expectedSnakePosX = validState.Snake.X;
        var expectedSnakePosY = validState.Snake.Y;
        var expectedSnakeVelX = validState.Snake.VelX;
        var expectedSnakeVelY = validState.Snake.VelY;
        var previousFruitPosX = validState.Fruit.X;
        var previousFruitPosY = validState.Fruit.Y;
        
        //Act
        var actionResult = service.VerifyData(validState);
        var okResult = (OkObjectResult)actionResult.Result;
        var newState = (StateConfig)okResult.Value;

        //Assert
        Assert.IsType<OkObjectResult>(okResult);
        Assert.IsType<StateConfig>(newState);
        Assert.Equal(expectedStatusCode, okResult.StatusCode);
        Assert.Equal(expectedScore, newState.Score);
        Assert.Equal(expectedGameId, newState.GameID);
        Assert.Equal(expectedWidth, newState.Width);
        Assert.Equal(expectedHeight, newState.Height);
        Assert.Equal(expectedSnakePosX,newState.Snake.X);
        Assert.Equal(expectedSnakePosY, newState.Snake.Y);
        Assert.Equal(expectedSnakeVelX, newState.Snake.VelX);
        Assert.Equal(expectedSnakeVelY, newState.Snake.VelY);
        Assert.NotEqual(previousFruitPosX, newState.Fruit.X);
        Assert.NotEqual(previousFruitPosY, newState.Fruit.Y);
    }

    [Fact]
    public void VerifyDataTests_WithFruitNoFoundData_ReturnNotFoundMessage()
    {
        //Arrange
        var service = GetService();
        var fruitNotFoundState = GetFruitNotFoundGameState();
        var expectedStatusCode = 404;
        var expectedMessage = MessageConfig.FruitNotFound;

        //Act
        var actionResult = service.VerifyData(fruitNotFoundState);
        var objectResult = (ObjectResult)actionResult.Result;

        //Assert
        Assert.IsType<ObjectResult>(objectResult);
        Assert.Equal(expectedStatusCode, objectResult.StatusCode);
        Assert.Equal(expectedMessage, objectResult.Value);

    }
    
    [Fact]
    public void VerifyDataTests_WithInvalidMoveData_ReturnInvalidMoveMessage()
    {
        //Arrange
        var service = GetService();
        var invalidSMoveState = GetInvalidMoveState();
        var expectedStatusCode = 418;
        var expectedMessage = MessageConfig.GameOverDirection;

        //Act
        var actionResult = service.VerifyData(invalidSMoveState);
        var objectResult = (ObjectResult)actionResult.Result;

        //Assert
        Assert.IsType<ObjectResult>(objectResult);
        Assert.Equal(expectedStatusCode, objectResult.StatusCode);
        Assert.Equal(expectedMessage, objectResult.Value);

    }
    
    [Fact]
    public void VerifyDataTests_WithOutOfBoundData_ReturnOutOfBoundMessage()
    {
        //Arrange
        var service = GetService();
        var outOfBoundState = GetOutOfBoundState();
        var expectedStatusCode = 418;
        var expectedMessage = MessageConfig.GameOverPosition;

        //Act
        var actionResult = service.VerifyData(outOfBoundState);
        var objectResult = (ObjectResult)actionResult.Result;

        //Assert
        Assert.IsType<ObjectResult>(objectResult);
        Assert.Equal(expectedStatusCode, objectResult.StatusCode);
        Assert.Equal(expectedMessage, objectResult.Value);

    }

    public ValidateConfig GetFruitNotFoundGameState()
    {
        var wrongSnakeInfo = new SnakeConfig()
        {
            VelX = 1,
            VelY = 0,
            X = 50,
            Y = 20,
        };

        var wrongFruitInfo = new FruitConfig()
        {
            X = 51,
            Y = 20,
        };

        var dummyTickListInfo = new List<VelocityTicks>
        {
            new() { VelX = 1, VelY = 0 },
            new() { VelX = 1, VelY = 0 },
            new() { VelX = 0, VelY = -1 },
            new() { VelX = -1, VelY = 0 },
            new() { VelX = -1, VelY = 0 },
            new() { VelX = 0, VelY = 1 },
        };

        var dummyGameState = new ValidateConfig()
        {
            GameID = "Example",
            Width = 200,
            Height = 200,
            Score = 50,
            Fruit = wrongFruitInfo,
            Snake = wrongSnakeInfo,
            Ticks = dummyTickListInfo
        };

        return dummyGameState;

    }
    
    public ValidateConfig GetInvalidMoveState()
    {
        var wrongSnakeInfo = new SnakeConfig()
        {
            VelX = 1,
            VelY = 0,
            X = 50,
            Y = 20,
        };

        var wrongFruitInfo = new FruitConfig()
        {
            X = 50,
            Y = 20,
        };

        var dummyTickListInfo = new List<VelocityTicks>
        {
            new() { VelX = 1, VelY = 0 },
            new() { VelX = 1, VelY = 0 },
            new() { VelX = 0, VelY = -1 },
            new() { VelX = -1, VelY = 0 },
            new() { VelX = 1, VelY = 0 },
            new() { VelX = 0, VelY = 1 },
        };

        var dummyGameState = new ValidateConfig()
        {
            GameID = "Example",
            Width = 200,
            Height = 200,
            Score = 50,
            Fruit = wrongFruitInfo,
            Snake = wrongSnakeInfo,
            Ticks = dummyTickListInfo
        };

        return dummyGameState;

    }

    public ValidateConfig GetOutOfBoundState()
    {
        var wrongSnakeInfo = new SnakeConfig()
        {
            VelX = 1,
            VelY = 0,
            X = 199,
            Y = 20,
        };

        var wrongFruitInfo = new FruitConfig()
        {
            X = 199,
            Y = 20,
        };

        var dummyTickListInfo = new List<VelocityTicks>
        {
            new() { VelX = 1, VelY = 0 },
            new() { VelX = 1, VelY = 0 },
            new() { VelX = 0, VelY = -1 },
            new() { VelX = -1, VelY = 0 },
            new() { VelX = -1, VelY = 0 },
            new() { VelX = 0, VelY = 1 },
        };

        var dummyGameState = new ValidateConfig()
        {
            GameID = "Example",
            Width = 200,
            Height = 200,
            Score = 50,
            Fruit = wrongFruitInfo,
            Snake = wrongSnakeInfo,
            Ticks = dummyTickListInfo
        };

        return dummyGameState;

    }
    public ValidateConfig GetValidGameState()
    {
        var wrongSnakeInfo = new SnakeConfig()
        {
            VelX = 1,
            VelY = 0,
            X = 78,
            Y = 20,
        };

        var wrongFruitInfo = new FruitConfig()
        {
            X = 78,
            Y = 20,
        };

        var dummyTickListInfo = new List<VelocityTicks>
        {
            new() { VelX = 1, VelY = 0 },
            new() { VelX = 1, VelY = 0 },
            new() { VelX = 0, VelY = -1 },
            new() { VelX = -1, VelY = 0 },
            new() { VelX = -1, VelY = 0 },
            new() { VelX = 0, VelY = 1 },
        };

        var dummyGameState = new ValidateConfig()
        {
            GameID = "Example",
            Width = 200,
            Height = 200,
            Score = 50,
            Fruit = wrongFruitInfo,
            Snake = wrongSnakeInfo,
            Ticks = dummyTickListInfo
        };

        return dummyGameState;

    }
}