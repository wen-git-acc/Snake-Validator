using Snake.Validator.Service.Payload;

namespace Snake.Validator.Service.Services;

public class NewGame : INewGame
{
    private readonly IGeneralHelper _generalHelper;

    public NewGame(IGeneralHelper generalHelper)
    {
        _generalHelper = generalHelper;
    }
    public State StartGame(int width, int height)
    {
        var newGameId = GenerateRandomGameId();
        var newFruitPosition = _generalHelper.GenerateFruitPosition(width, height);
        var newSnakeInformation = new Payload.Snake()
        {
            VelX = 1,
            VelY = 0,
            X = 0,
            Y = 0,
        };

        return new State()
        {
            GameID = newGameId,
            Width = width,
            Height = height,
            Score = 0,
            Fruit = newFruitPosition,
            Snake = newSnakeInformation
        };
    }

    private string GenerateRandomGameId()
    {
        return Guid.NewGuid().ToString();
    }
}