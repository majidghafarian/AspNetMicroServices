
namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id= Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
        }
        public IntegrationBaseEvent(Guid id, DateTime createdate)
        {
            Id = id;
            CreateDate = createdate;
        }

        public Guid Id { get; private set; }
        public DateTime CreateDate { get; private set; }
    }
}
