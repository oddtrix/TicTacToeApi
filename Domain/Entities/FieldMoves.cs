namespace Domain.Entities
{
    public class FieldMoves : BaseEntity
    {
        public Guid FieldId { get; set; }

        public Field Field { get; set; }

        public ICollection<Cell> Cells { get; set; }
    }
}
