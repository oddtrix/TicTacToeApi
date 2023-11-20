namespace Domain.Entities
{
    public class FieldMoves
    {
        public Guid Id { get; set; }

        public Guid FieldId { get; set; }

        public Field Field { get; set; }

        public ICollection<Cell> Cells { get; set; }
    }
}
