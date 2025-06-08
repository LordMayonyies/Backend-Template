using MediatR;
using Microsoft.Extensions.Logging;

namespace Mayonyies.Core.Shared;

public class MediatRDomainEventDispatcher(IMediator mediator, ILogger<MediatRDomainEventDispatcher> logger)
    : IDomainEventDispatcher
{
    public async Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
            if (entity is HasDomainEventsBase hasDomainEvents)
            {
                var events = hasDomainEvents.DomainEvents.ToArray();
                hasDomainEvents.ClearDomainEvents();

                foreach (var domainEvent in events)
                    await mediator.Publish(domainEvent).ConfigureAwait(false);
            }
            else
            {
                logger.LogError(
                    "Entity of type {EntityType} does not inherit from {BaseType}. Unable to clear domain events.",
                    entity.GetType().Name,
                    nameof(HasDomainEventsBase));
            }
    }
}