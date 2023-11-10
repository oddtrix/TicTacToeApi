namespace TicTacToeApi.Models.Domain
{
    public class FieldMoves
    {
        public Guid FieldId { get; set; }

        public Field Field { get; set; }

        public IEnumerable<Cell> Cells { get; set; }
    }
}
