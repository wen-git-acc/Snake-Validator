using Snake.Validator.Service.Response;

namespace Snake.Validator.Service.Services;

public interface IGeneralHelper
{
    public FruitConfig GenerateFruitPosition(int width, int height);
}