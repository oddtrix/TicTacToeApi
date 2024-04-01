namespace Domain.DTOs.Chat
{
    public class MessageSendingDTO : BaseDTO
    {
        public string MessageBody { get; set; }

        public DateTime DateTime { get; set; }

        public Guid ChatId { get; set; }

        public Guid PlayerId { get; set; }
    }
}
