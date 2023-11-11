using Microsoft.AspNetCore.Mvc;
using Snake.Validator.Service.Payload;
using Snake.Validator.Service.Response;

namespace Snake.Validator.Service.Services;

public interface IValidator
{
    public ActionResult<StateConfig> VerifyData(ValidateConfig state);

    public (bool isValid, string? description) IsMoveValid(int width, int height, SnakeConfig snakeInfo, List<VelocityTicks> ticks);

    public bool IsFruitFound(FruitConfig fruit, SnakeConfig snake);
}