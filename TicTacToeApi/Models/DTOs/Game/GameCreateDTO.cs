using TicTacToeApi.BusinessLayer.Enums;

namespace TicTacToeApi.Models.DTOs.Game
{
    public class GameCreateDTO
    {
        public Guid Id { get; set; }

        public GameStatus GameStatus { get; set; } = GameStatus.Created;

        public bool isPrivate { get; set; } = false;
    }
}
