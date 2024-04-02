namespace Domain.Entities
{
    public class Player : BaseEntity
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }
        
        public string AvatarURL { get; set; }

        public ICollection<GamePlayerJunction> GamesPlayers { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<Cell> Cells { get; set; }
    }
}
