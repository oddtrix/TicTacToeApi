using TicTacToeApi.BusinessLayer.Enums;

namespace TicTacToeApi.Models.DTOs.Game
{
    public class GameUpdateDTO
    {
        public Guid Id { get; set; }

        public GameStatus GameStatus { get; set; }

        public bool isPrivate { get; set; }

        public Domain.Player Winner { get; set; }
    }
}
