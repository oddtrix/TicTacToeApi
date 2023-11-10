namespace TicTacToeApi.Models.Domain
{
    public class Field
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }

        public Game Game { get; set; }
    }
}
