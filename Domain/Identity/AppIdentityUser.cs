using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppIdentityUser : IdentityUser<Guid>
    {
        public int Rating { get; set; }

        public string AvatarURL { get; set; }
    }
}
