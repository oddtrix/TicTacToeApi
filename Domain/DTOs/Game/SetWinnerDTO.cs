namespace Domain.DTOs.Game
{
    public class SetWinnerDTO
    {
        public Guid WinnerId { get; set; }

        public Guid LoserId { get; set; }

        public Guid GameId { get; set; }
    }
}
