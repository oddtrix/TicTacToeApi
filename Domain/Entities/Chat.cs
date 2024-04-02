namespace Domain.Entities
{
    public class Chat : BaseEntity
    {
        public Game Game { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
