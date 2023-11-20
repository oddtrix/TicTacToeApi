using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace TicTacToeApi.Models.EntityConfiguration
{
    public class FieldEntityConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasOne(f => f.Game)
                .WithOne(g => g.Field)
                .HasForeignKey<Game>(g => g.FieldId);

            builder.HasOne(f => f.FieldMoves)
                .WithOne(fm => fm.Field)
                .HasForeignKey<FieldMoves>(fm => fm.FieldId);
        }
    }
}
