using Microsoft.AspNetCore.Mvc;
using Snake.Validator.Service.Response;

namespace Snake.Validator.Service.Services;

public class NewGame : INewGame
{
    private readonly IGeneralHelper _generalHelper;

    public NewGame(IGeneralHelper generalHelper)
    {
        _generalHelper = generalHelper;
    }
    public ActionResult<StateConfig> StartGame(int width, int height)
    {
        var newConfig = GenerateNewGameState(width, height);
        
        return new OkObjectResult(newConfig);
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