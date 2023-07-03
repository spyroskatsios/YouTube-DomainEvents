using DomainEvents.Domain.Events;

namespace DomainEvents.Domain.Entities;

public class Person : IHasDomainEvent
{
    public int Id { get; set; }
    public string FullName { get; private set; }
    public int Age { get; private set; }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    public Person(string fullName, int age)
    {
        FullName = fullName;
        Age = age;
    }

    public void Update(string fullName, int age)
    {
        FullName = fullName;
        Age = age;
        DomainEvents.Add(new PersonUpdatedEvent(this));
    }
}