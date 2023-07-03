using DomainEvents.Application.Models;
using DomainEvents.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DomainEvents.Application.DomainEventHandlers;

public class DomainEventNotificationHandler : INotificationHandler<DomainEventNotification<PersonUpdatedEvent>>
{
    private readonly ILogger<DomainEventNotificationHandler> _logger;

    public DomainEventNotificationHandler(ILogger<DomainEventNotificationHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<PersonUpdatedEvent> notification, CancellationToken cancellationToken)
    {
        var @event = notification.DomainEvent;
        _logger.LogInformation("Person updated: {personId}", @event.UpdatedPerson.Id);
        return Task.CompletedTask;
    }
}