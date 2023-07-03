using DomainEvents.Application.Interfaces;
using DomainEvents.Application.Models;
using DomainEvents.Domain.Entities;
using DomainEvents.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainEvents.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Person> People { get; set; }

    private readonly IMediator _mediator;

    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Person>()
            .HasKey(x => x.Id);

        builder.Entity<Person>()
            .Ignore(x => x.DomainEvents);

        
        builder.Entity<Person>()
            .ToTable("People");
        
        base.OnModelCreating(builder);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        var events = ChangeTracker.Entries<IHasDomainEvent>()
            .Select(x=>x.Entity.DomainEvents)
            .SelectMany(x=>x)
            .Where(x=>!x.IsPublished)
            .ToList();
        
        await SaveChangesAsync(cancellationToken);

        foreach (var @event in events)
        {
            var notification = GetNotificationEvent(@event);
            await _mediator.Publish(notification, cancellationToken);
            @event.IsPublished = true;
        }
    }

    private INotification GetNotificationEvent(DomainEvent @event)
    {
        var eventType = @event.GetType();

        var notification =
            Activator.CreateInstance(typeof(DomainEventNotification<>).MakeGenericType(eventType), @event) as
                INotification;

        return notification!;
    }
}