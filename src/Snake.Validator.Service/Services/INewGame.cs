using Microsoft.AspNetCore.Mvc;
using Snake.Validator.Service.Response;

namespace Snake.Validator.Service.Services;

public interface INewGame
{
    public ActionResult<StateConfig> StartGame(int width, int height);
}