using TicTacToeApi.BusinessLayer.Enums;

namespace TicTacToeApi.Models.Domain
{
    public class Cell
    {
        public Guid Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public CellState Value { get; set; }

        public Guid FieldId { get; set; }

        public FieldMoves FieldMoves { get; set; }

        public Guid PlayerId {  get; set; }

        public Player Player { get; set; }
    }
}
