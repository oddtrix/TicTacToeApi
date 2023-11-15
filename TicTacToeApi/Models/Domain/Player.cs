namespace TicTacToeApi.Models.Domain
{
    public class Player
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }

        public string AvatarURL { get; set; }

        public virtual ICollection<GamePlayerJunction> GamesPlayers { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Cell> Cells { get; set; }
    }
}
