using TicTacToeApi.BusinessLayer.Interfaces;
using TicTacToeApi.Models.Domain;
using TicTacToeApi.Models.DTOs.Game;

namespace TicTacToeApi.BusinessLayer.Services
{
    public class GameService : IGameService
    {
        private readonly IEntityService<Game, GameCreateDTO, GameUpdateDTO> repository;

        public GameService(IEntityService<Game, GameCreateDTO, GameUpdateDTO> repository) 
        {
            this.repository = repository;
        }

        public Game CreateGame(GameCreateDTO createDTO)
        {
            var game = this.repository.Create(createDTO);
            return game;
        }
    }
}
