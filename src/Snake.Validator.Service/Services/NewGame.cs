using Microsoft.AspNetCore.Mvc;
using Snake.Validator.Service.Messages;
using Snake.Validator.Service.Response;

namespace Snake.Validator.Service.Services;

public class NewGame : INewGame
{
    private readonly IGeneralHelper _generalHelper;
    public int minimumWidth = 5;
    public int minimumHeight = 5;

    public NewGame(IGeneralHelper generalHelper)
    {
        _generalHelper = generalHelper;
    }

    public ActionResult<StateConfig> StartGame(int width, int height)
    {
        var IsSizeCorrect = IsWidthHeightValid(width, height);

        if (!IsSizeCorrect)
        {
            return new ObjectResult(MessageConfig.IncorrectStartingSize) { StatusCode = 400 };
        }

        var newConfig = GenerateNewGameState(width, height);
        
        return new OkObjectResult(newConfig);
    }

    private bool IsWidthHeightValid(int width, int height)
    {
        return width >= minimumWidth && height >= minimumHeight;
    }

    private StateConfig GenerateNewGameState(int width, int height)
    {
        var newGameId = GenerateRandomGameId();
        var newFruitPosition = _generalHelper.GenerateFruitPosition(width, height);
        var newSnakeInformation = new SnakeConfig()
        {
            VelX = 1,
            VelY = 0,
            X = 0,
            Y = 0,
        };
        StateConfig newConfig = new StateConfig()
        {
            GameID = newGameId,
            Width = width,
            Height = height,
            Score = 0,
            Fruit = newFruitPosition,
            Snake = newSnakeInformation
        };

        return newConfig;
    }

    private string GenerateRandomGameId()
    {
        return Guid.NewGuid().ToString();
    }
}