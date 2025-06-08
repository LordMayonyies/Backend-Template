namespace Mayonyies.Core.Shared;

/// <summary>
///     A simple interface for sending domain events.
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents);
}