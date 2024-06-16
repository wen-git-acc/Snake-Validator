using Microsoft.AspNetCore.Mvc;
using Snake.Validator.Service.Payload;
using Snake.Validator.Service.Response;

namespace Snake.Validator.Service.Services;

public interface ISnakeValidator
{
    public ActionResult<StateConfig> VerifyData(ValidateConfig state);

}