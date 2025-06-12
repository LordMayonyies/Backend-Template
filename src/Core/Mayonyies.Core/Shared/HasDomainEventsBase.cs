namespace Mayonyies.Core.Shared;

public abstract class HasDomainEventsBase
{
    private readonly List<DomainEventBase> _domainEvents = [];
    
    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

    public IReadOnlyList<DomainEventBase> GetAndClearDomainEvents()
    {
        var domainEvents = _domainEvents.ToList();
        _domainEvents.Clear();
        return domainEvents;
    }
}