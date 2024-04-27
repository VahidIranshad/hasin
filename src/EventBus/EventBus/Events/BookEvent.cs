namespace EventBus.Events
{
    public class BookEvent : IntegrationEvent
    {
        public int BookId { get; set; }
        public required string BookValue { get; set; }
    }
}
