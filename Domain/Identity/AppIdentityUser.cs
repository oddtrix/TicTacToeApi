using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppIdentityUser : IdentityUser<Guid>
    {
        public int Rating { get; set; } = 100;

        public string AvatarURL { get; set; } = "https://cdn0.iconfinder.com/data/icons/artificial-intelligence-and-machine-learning-glyph/48/AI-Icon-17-512.png";
    }
}
