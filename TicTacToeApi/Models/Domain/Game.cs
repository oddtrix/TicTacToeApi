using TicTacToeApi.BusinessLayer.Enums;

namespace TicTacToeApi.Models.Domain
{
    public class Game
    {
        public Guid Id { get; set; }

        public GameStatus GameStatus { get; set; }

        public bool isPrivate { get; set; }

        public Player Winner { get; set; }

        public Guid PlayerId_1 { get; set; }

        public Player Player_1 { get; set; }

        public Guid PlayerId_2 { get; set; }

        public Player Player_2 { get; set; }

        public Guid ChatId { get; set; }

        public Chat Chat {  get; set; }

        public Guid FieldId { get; set; }

        public Field Field { get; set; }
    }
}
