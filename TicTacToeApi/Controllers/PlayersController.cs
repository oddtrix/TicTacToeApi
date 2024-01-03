using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.DTOs.Player;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TicTacToeApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        public IMapper Mapper;

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
            //var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
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
        [HttpGet]
        public IActionResult History()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var game = this.playerService.History(userId);
            return Ok(game);
        }
    }
}
