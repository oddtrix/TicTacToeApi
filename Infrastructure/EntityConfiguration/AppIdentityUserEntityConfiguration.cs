using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Identity;

namespace TicTacToeApi.Models.EntityConfiguration
{
    public class AppIdentityUserEntityConfiguration : IEntityTypeConfiguration<AppIdentityUser>
    {
        public void Configure(EntityTypeBuilder<AppIdentityUser> builder)
        {
            builder.Property(p => p.Rating).HasDefaultValue(100);
            builder.Property(p => p.AvatarURL).HasDefaultValue("https://static.wikia.nocookie.net/dota2_gamepedia/images/3/33/Bot_passive_icon.png/revision/latest?cb=20170711173119");
        }
    }
}
