namespace Domain.Entities
{
    public class Chat : BaseEntity
    {
        public Game Game { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
