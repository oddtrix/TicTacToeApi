using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.DTOs.Player;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToeApi.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]/[action]")]
    public class PlayersController : ControllerBase
    {
        private readonly IMapper Mapper;

        private readonly IPlayerService playerService;

        private readonly IEntityService<Player> entityService;

        public PlayersController(IEntityService<Player> entityService, IMapper Mapper, IPlayerService playerService)
        {
            this.Mapper = Mapper;
            this.entityService = entityService;
            this.playerService = playerService;
        }

        [HttpGet] 
        public IActionResult GetAll()
        {
            var players = this.entityService.GetAll();
            return Ok(players);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var player = this.entityService.GetById(id);
            return Ok(player);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlayerUpdateDTO playerUpdateDTO)
        {
            var player = this.Mapper.Map<Player>(playerUpdateDTO);
            var updatedPlayer = this.entityService.Update(player);
            return Ok(updatedPlayer);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            this.entityService.Delete(id);
            return Ok($"Player with id: {id} was deleted");
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id:guid}")]
        public IActionResult History(Guid id)
        {
            var game = this.playerService.History(id);
            return Ok(game);
        }
    }
}
