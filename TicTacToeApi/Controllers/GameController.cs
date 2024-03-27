using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.DTOs.Field;
using Domain.DTOs.Game;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TicTacToeApi.Hubs;

namespace TicTacToeApi.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]/[action]")]
    public class GameController : ControllerBase
    {
        private readonly IMapper Mapper;

        private readonly IGameService gameService;

        private readonly IHubContext<GameHub> hubContext;

        public GameController(IGameService gameService, IMapper Mapper, IHubContext<GameHub> hubContext) 
        {
            this.Mapper = Mapper;
            this.gameService = gameService;
            this.hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult GetGameById(Guid gameId)
        {
            var game = this.gameService.FindGameById(gameId);
            return Ok(game);
        }

        [HttpPost]
        public IActionResult CreateGame([FromBody] GameCreateDTO createDTO)
        {
            var userNameIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString();
            var userId = Guid.Parse(userNameIdentifier);

            var game = this.Mapper.Map<Game>(createDTO);
            this.gameService.CreateGame(game);
            this.gameService.CreateGamePlayer(game.Id, userId);

            if (game.GamesPlayers.Count == 1)
            {
                game.GameStatus = GameStatus.Pending;
            }
            return Ok(game);
        }

        [HttpPost]
        public IActionResult JoinToGame([FromBody] GameIdDTO gameIdDTO)
        {
            var userNameIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString();
            var userId = Guid.Parse(userNameIdentifier);

            var gameId = Guid.Parse(gameIdDTO.GameId.ToString());
            var game = this.gameService.FindGameById(gameId);
            this.gameService.JoinToGame(userId, game);
            return Ok(game);
        }

        [HttpPost]
        public IActionResult MakeMove([FromBody] FieldUpdateDTO fieldUpdateDTO)
        {
            var game = this.gameService.MakeMove(fieldUpdateDTO.GameId, 
                fieldUpdateDTO.FieldId, fieldUpdateDTO.FieldMovesId, 
                fieldUpdateDTO.PlayerId, fieldUpdateDTO.index);
            return Ok(game);
        }

        [HttpPost]
        public IActionResult SetWinner([FromBody] SetWinnerDTO setWinnerDTO)
        {
            var game = this.gameService.SetWinner(setWinnerDTO.WinnerId, setWinnerDTO.LoserId, setWinnerDTO.GameId);
            return Ok(game);
        }

        [HttpPost]
        public IActionResult SetDraw([FromBody] GameIdDTO gameIdDTO)
        {
            var game = this.gameService.SetDraw(gameIdDTO.GameId);
            return Ok(game);
        }

        [HttpPost]
        public IActionResult CancelGame(Guid gameId)
        {
            var game = this.gameService.CancelGame(gameId);
            return Ok(game);
        }

        [HttpGet]
        public IActionResult GetOpenGames()
        {
            var games = this.gameService.GetOpenGames();
            return Ok(games);
        }
    }
}
