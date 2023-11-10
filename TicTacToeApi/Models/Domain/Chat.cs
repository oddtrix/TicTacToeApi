namespace TicTacToeApi.Models.Domain
{
    public class Chat
    {
        public Guid Id { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
