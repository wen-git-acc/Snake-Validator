using Snake.Validator.Service.Payload;

namespace Snake.Validator.Service.Services;

public interface IGeneralHelper
{
    public Fruit GenerateFruitPosition(int width, int height);
}