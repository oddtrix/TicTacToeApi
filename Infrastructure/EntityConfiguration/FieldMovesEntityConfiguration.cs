using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TicTacToeApi.Models.EntityConfiguration
{
    public class FieldMovesEntityConfiguration : IEntityTypeConfiguration<FieldMoves>
    {
        public void Configure(EntityTypeBuilder<FieldMoves> builder)
        {
            builder.HasKey(fm => fm.Id);

            builder.HasMany(fm => fm.Cells)
                .WithOne(c => c.FieldMoves)
                .HasForeignKey(c => c.FieldId);
        }
    }
}
