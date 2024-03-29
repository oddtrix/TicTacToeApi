using ApplicationCore.Enums;

namespace Domain.Entities
{
    public class Game : BaseEntity
    {
        public GameStatus GameStatus { get; set; }

        public bool IsPrivate { get; set; }

        public Player Winner { get; set; }

        public virtual ICollection<GamePlayerJunction> GamesPlayers { get; set; }

        public Guid ChatId { get; set; }

        public Chat Chat { get; set; }

        public Guid FieldId { get; set; }

        public Field Field { get; set; }

        public int StrokeNumber { get; set; }

        public Guid PlayerQueueId { get; set; }
    }
}
