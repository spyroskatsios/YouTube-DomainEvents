namespace DomainEvents.Domain.Events;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}

public abstract class DomainEvent
{
    public bool IsPublished { get; set; }
}