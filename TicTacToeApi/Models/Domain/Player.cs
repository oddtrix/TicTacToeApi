namespace TicTacToeApi.Models.Domain
{
    public class Player
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Rating { get; set; }

        public string AvatarURL { get; set; }

        public IEnumerable<Game> Games { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<Cell> Cells { get; set; }
    }
}
