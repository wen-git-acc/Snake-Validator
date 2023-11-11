using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public async Task<ActionResult<StateConfig>> ValidateState([FromBody] ValidateConfig state)
        {
            try
            {
                var response = _validator.VerifyData(state);
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
       
        }   
        
        [HttpGet]
        [ActionName("New")]
        public async Task<ActionResult<StateConfig>> GetNewGame(
            [FromQuery(Name = "w"), BindRequired] int width, 
            [FromQuery(Name = "h"), BindRequired] int height)
        {
            try
            {
                var response = _newGame.StartGame(width, height);
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
         
        }
    }
}