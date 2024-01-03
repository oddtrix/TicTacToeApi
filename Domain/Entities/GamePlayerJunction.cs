namespace Domain.Entities
{
    public class GamePlayerJunction
    {
        public Guid PlayerId { get; set; }

        public Player Player { get; set; }

        public Guid GameId { get; set; }

        public Game Game { get; set; }
    }
}
