using DomainEvents.Domain.Entities;

namespace DomainEvents.Domain.Events;

public class PersonUpdatedEvent : DomainEvent
{
    public Person UpdatedPerson { get; }

    public PersonUpdatedEvent(Person updatedPerson)
    {
        UpdatedPerson = updatedPerson;
    }
}