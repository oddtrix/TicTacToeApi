namespace Domain.DTOs.Player
{
    public class PlayerCreateDTO : BaseDTO
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }

        public string AvatarURL { get; set; }
    }
}
