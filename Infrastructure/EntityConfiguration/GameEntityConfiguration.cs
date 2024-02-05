using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TicTacToeApi.Models.EntityConfiguration
{
    public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.HasMany(g => g.GamesPlayers)
                .WithOne(gp => gp.Game)
                .HasForeignKey(gp => gp.GameId);
        }
    }
}
