using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Chat;
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

        public GameController(IGameService gameService, IMapper Mapper) 
        {
            this.Mapper = Mapper;
            this.gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetGameById(Guid gameId)
        {
            var game = this.gameService.FindGameByIdWithInclude(gameId);
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
        public IActionResult JoinToGame([FromBody] BaseDTO gameIdDTO)
        {
            var userNameIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString();
            var userId = Guid.Parse(userNameIdentifier);

            var gameId = Guid.Parse(gameIdDTO.Id.ToString());
            var game = this.gameService.FindGameByIdWithInclude(gameId);
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
        public IActionResult SetDraw([FromBody] BaseDTO gameIdDTO)
        {
            var game = this.gameService.SetDraw(gameIdDTO.Id);
            return Ok(game);
        }

        [HttpPost]
        public IActionResult CancelGame(Guid gameId)
        {
            var game = this.gameService.CancelGame(gameId);
            return Ok(game);
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] MessageSendingDTO messageDTO)
        {
            var game = this.gameService.SendMessage(messageDTO.Id, messageDTO.MessageBody, messageDTO.ChatId, messageDTO.PlayerId);
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
