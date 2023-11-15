using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicTacToeApi.BusinessLayer.Interfaces;
using TicTacToeApi.Models.DTOs.Game;

/*namespace TicTacToeApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IGameService gameService;

        public GameController(IGameService gameService) 
        {
            this.gameService = gameService;
        }

        [HttpPost]
        public IActionResult StartGame([FromBody] GameCreateDTO createDTO)
        {
            var userNameIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString();
            var userId = Guid.Parse(userNameIdentifier);
            gameService.CreateGame(createDTO);

            return Ok(gameService.CreateGame(createDTO));
        }
    }
}*/
