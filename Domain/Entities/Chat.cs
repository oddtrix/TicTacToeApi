namespace Domain.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }

        public Game Game { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
