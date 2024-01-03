using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TicTacToeApi.Models.EntityConfiguration
{
    public class GamePlayerJunctionEntityConfiguration : IEntityTypeConfiguration<GamePlayerJunction>
    {
        public void Configure(EntityTypeBuilder<GamePlayerJunction> builder)
        {
            builder.HasKey(gp => new { gp.PlayerId, gp.GameId });
        }
    }
}
