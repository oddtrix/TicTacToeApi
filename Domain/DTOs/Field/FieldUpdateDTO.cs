using ApplicationCore.Enums;

namespace Domain.DTOs.Field
{
    public class FieldUpdateDTO
    {
        public Guid GameId { get; set; }

        public Guid FieldId { get; set; }

        public Guid FieldMovesId { get; set; }

        public Guid PlayerId { get; set; }

        public int index { get; set; }
    }
}