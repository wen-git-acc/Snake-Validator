using Microsoft.AspNetCore.Mvc;
using Snake.Validator.Service.Payload;
using Snake.Validator.Service.Response;
using Snake.Validator.Service.Services;


namespace Snake.Validator.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class SnakeGameController : ControllerBase
    {
        private readonly ILogger<SnakeGameController> _logger;
        private readonly INewGame _newGame;
        private readonly IValidator _validator;

        public SnakeGameController(ILogger<SnakeGameController> logger, INewGame newGame, IValidator validator)
        {
            _logger = logger;
            _newGame = newGame;
            _validator = validator;
        }

        [HttpPost]
        [ActionName("Validate")]
        public ActionResult<StateConfig> ValidateState([FromBody] ValidateConfig data)
        {
            var response = _validator.MoveValidation(data);           
            return response;
        }   
        
        [HttpGet]
        [ActionName("New")]
        public ActionResult<StateConfig> GetNewGame(
            [FromQuery(Name = "w")] int w, 
            [FromQuery(Name = "h")] int h)
        {
            var response = _newGame.StartGame(w, h);
            return response;
        }
    }
}