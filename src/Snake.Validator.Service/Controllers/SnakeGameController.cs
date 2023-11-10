using Microsoft.AspNetCore.Mvc;
using Snake.Validator.Service.Payload;
using Snake.Validator.Service.Services;


namespace Snake.Validator.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class SnakeGameController : ControllerBase
    {
        private readonly ILogger<SnakeGameController> _logger;
        private readonly INewGame _newGame;

        public SnakeGameController(ILogger<SnakeGameController> logger, INewGame newGame)
        {
            _logger = logger;
            _newGame = newGame;
        }

        [HttpPost]
        [ActionName("Validate")]
        public ActionResult<State> ValidateState([FromBody] State gameState)
        {
            Console.WriteLine(gameState);
            var x = gameState.GameID;
            var y = gameState.Fruit.X;
            Console.WriteLine(y);
            Console.WriteLine(x);
            return Ok(gameState);
        }   
        
        [HttpGet]
        [ActionName("New")]
        public ActionResult<State> ValidateState(
            [FromQuery(Name = "w")] int w, 
            [FromQuery(Name = "h")] int h)
        {
            var response = _newGame.StartGame(w, h);
            return Ok(response);
        }
    }
}