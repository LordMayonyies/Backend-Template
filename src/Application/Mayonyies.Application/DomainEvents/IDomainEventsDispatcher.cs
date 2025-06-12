using Mayonyies.Core.Shared;

namespace Mayonyies.Application.DomainEvents;

public interface IDomainEventsDispatcher
{
    Task DispatchAsync(
        IEnumerable<DomainEventBase> domainEvents,
        CancellationToken cancellationToken = default);
}