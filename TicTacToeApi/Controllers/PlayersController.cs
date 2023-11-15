using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicTacToeApi.BusinessLayer.Interfaces;
using TicTacToeApi.Models.Domain;
using TicTacToeApi.Models.DTOs.Player;

namespace TicTacToeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IEntityService<Player, PlayerCreateDTO, PlayerUpdateDTO> playerService;

        public PlayersController(IEntityService<Player, PlayerCreateDTO, PlayerUpdateDTO> playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet] 
        public IActionResult GetAll()
        {
            var players = this.playerService.GetAll();
            return Ok(players);
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var player = this.playerService.GetById(id);
            return Ok(player);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlayerUpdateDTO playerUpdateDTO)
        {
            //var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var updatedPlayer = this.playerService.Update(playerUpdateDTO);
            return Ok(updatedPlayer);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            this.playerService.Delete(id);
            return Ok($"Player with id: {id} was deleted");
        }
    }
}
