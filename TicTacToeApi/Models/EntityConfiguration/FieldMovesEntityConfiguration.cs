using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToeApi.Models.Domain;

namespace TicTacToeApi.Models.EntityConfiguration
{
    public class FieldMovesEntityConfiguration : IEntityTypeConfiguration<FieldMoves>
    {
        public void Configure(EntityTypeBuilder<FieldMoves> builder)
        {
            builder.HasKey(fm => fm.FieldId);

            builder.HasMany(fm => fm.Cells)
                .WithOne(c => c.FieldMoves)
                .HasForeignKey(c => c.FieldId);
        }
    }
}
