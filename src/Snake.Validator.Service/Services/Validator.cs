using Microsoft.AspNetCore.Mvc;
using Snake.Validator.Service.Messages;
using Snake.Validator.Service.Payload;
using Snake.Validator.Service.Response;

namespace Snake.Validator.Service.Services;

public class Validator : IValidator
{
    private readonly IGeneralHelper _generalHelper;
    private readonly string _loseByPosition = "out";
    private readonly string _loseByDirection = "direction";
    public Validator(IGeneralHelper generalHelper)
    {
        _generalHelper = generalHelper;
    }
    public ActionResult<StateConfig> MoveValidation(ValidateConfig data)
    {
        var move = IsMoveValid(data.Width, data.Height, data.Snake, data.Ticks);

        if (!move.isValid)
        {
            var responseMessage = move.description == _loseByPosition ? MessageConfig.GameOverPosition : MessageConfig.GameOverDirection;
            return new ObjectResult(responseMessage) { StatusCode = 418 };
        }




        return new OkResult();
    }

    public (bool isValid, string? description) IsMoveValid(int width, int height, SnakeConfig snakeInfo, List<VelocityTicks> ticks)
    {
        var currentPosX = snakeInfo.X;
        var currentPosY = snakeInfo.Y;
        var currentSpeedX = snakeInfo.VelX;
        var currentSpeedY = snakeInfo.VelY;

        var ticksLength = ticks.Count;

        if (currentPosX < 0
            || currentPosX > width
            || currentPosY < 0
            || currentPosY > height) return (false, _loseByPosition);

        for (var i = ticksLength - 1; i >= 0; i--)
        {
            int previousSpeedX = ticks[i].VelX;
            int previousSpeedY = ticks[i].VelY;

            var isHalfRotation = ((currentSpeedX == -previousSpeedX && previousSpeedX != 0) 
                                  || currentSpeedY == -previousSpeedY && previousSpeedY != 0);

            if (isHalfRotation)
            {
                return (false, _loseByDirection);
            }

            int previousPosX = currentPosX - previousSpeedX;
            int previousPosY = currentPosY + previousSpeedY; //Due to unconventional direction specification

            var isOutOfBound = (previousPosX < 0 
                                || previousPosX > width 
                                || previousPosY < 0 
                                || previousPosY > height);

            if (isOutOfBound)
            {
                return (false, _loseByPosition);
            }

            currentSpeedX = previousSpeedX;
            currentSpeedY = previousSpeedY;

            currentPosX = previousPosX;
            currentPosY = previousPosY;

        }

        return (true, null);
    }
}