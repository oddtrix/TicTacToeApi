namespace Domain.Entities
{
    public class Player : BaseEntity
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }

        public string AvatarURL { get; set; }

        public virtual ICollection<GamePlayerJunction> GamesPlayers { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Cell> Cells { get; set; }
    }
}
