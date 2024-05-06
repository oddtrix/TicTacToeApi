using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.DTOs.Player;
using Domain.DTOs.Services;
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

        public PlayersController(IMapper Mapper, IPlayerService playerService)
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

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var player = this.playerService.GetById(id);
            return Ok(player);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlayerUpdateDTO playerUpdateDTO)
        {
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

        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id:guid}")]
        public IActionResult History(Guid id, int page = 1, int pageSize = 6)
        {
            var (gameHistory, gameHistoryCount) = this.playerService.History(id, page, pageSize);
            var dto = new PaginationDTO { 
                Page = page,
                PageSize = pageSize,
                TotalPages = gameHistoryCount,
                Items = gameHistory 
            };

            return Ok(dto);
        }
    }
}
