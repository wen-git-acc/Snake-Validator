using Microsoft.AspNetCore.Mvc;
using Snake.Validator.Service.Payload;
using Snake.Validator.Service.Response;

namespace Snake.Validator.Service.Services;

public interface IValidator
{
    public ActionResult<StateConfig> MoveValidation(ValidateConfig data);

    public (bool isValid, string? description) IsMoveValid(int width, int height, SnakeConfig snakeInfo, List<VelocityTicks> ticks);
}