namespace TicTacToeApi.Models.DTOs.Player
{
    public class PlayerCreateDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }

        public string AvatarURL { get; set; }
    }
}
