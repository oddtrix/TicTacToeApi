namespace Domain.Entities
{
    public class Field
    {
        public Guid Id { get; set; }

        public FieldMoves FieldMoves { get; set; }

        public Game Game { get; set; }
    }
}
