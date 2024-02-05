using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.DTOs.Game;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TicTacToeApi.Hubs;

namespace TicTacToeApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        public IMapper Mapper;

        private readonly IGameService gameService;

        private readonly IHubContext<GameHub> _hubContext;

        public GameController(IGameService gameService, IMapper Mapper, IHubContext<GameHub> hubContext) 
        {
            this.Mapper = Mapper;
            this.gameService = gameService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult GetGameById(Guid gameId)
        {
            var game = this.gameService.FindGameById(gameId);
            //_hubContext.Clients.Group(game.ToString()).SendAsync("ReceiveGameState", game);
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

            if (game.GamesPlayers.Count() == 1)
            {
                //this.gameService.UpdateGameState(new GameUpdateDTO(), game.Id, GameStatus.Pending);
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
        public IActionResult SetWinner(Guid winnerId, Guid loserId, Guid gameId)
        {
            var game = this.gameService.SetWinner(winnerId, loserId, gameId);
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
