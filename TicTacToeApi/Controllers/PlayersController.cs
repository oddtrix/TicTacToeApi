using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.DTOs.Player;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace TicTacToeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        public IMapper Mapper;

        private IEntityService<Player> playerService;

        public PlayersController(IEntityService<Player> playerService, IMapper Mapper)
        {
            this.Mapper = Mapper;
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
            var player = this.Mapper.Map<Player>(playerUpdateDTO);
            var updatedPlayer = this.playerService.Update(player);
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
