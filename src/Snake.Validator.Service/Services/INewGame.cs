using Snake.Validator.Service.Payload;

namespace Snake.Validator.Service.Services;

public interface INewGame
{

    public State StartGame(int width, int height);

}