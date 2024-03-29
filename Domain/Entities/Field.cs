namespace Domain.Entities
{
    public class Field : BaseEntity
    {
        public FieldMoves FieldMoves { get; set; }

        public Game Game { get; set; }
    }
}
