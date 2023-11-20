using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TicTacToeApi.Models.EntityConfiguration
{
    public class PlayerEntityConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.GamesPlayers)
                .WithOne(gp => gp.Player)
                .HasForeignKey(gp => gp.PlayerId);

            builder.HasMany(p => p.Messages)
                .WithOne(m => m.Player)
                .HasForeignKey(m => m.PlayerId);

            builder.HasMany(p => p.Cells)
               .WithOne(c => c.Player)
               .HasForeignKey(c => c.PlayerId);

            builder.Property(p => p.Rating).HasDefaultValue(100);
            builder.Property(p => p.AvatarURL).HasDefaultValue("https://static.wikia.nocookie.net/dota2_gamepedia/images/3/33/Bot_passive_icon.png/revision/latest?cb=20170711173119");
        }
    }
}
