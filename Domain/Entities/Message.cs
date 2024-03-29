namespace Domain.Entities
{
    public class Message : BaseEntity
    {
        public string MessageBody { get; set; }

        public DateTime DateTime { get; set; }

        public Guid ChatId { get; set; }

        public Chat Chat { get; set; }

        public Guid PlayerId { get; set; }

        public Player Player { get; set; }
    }
}
