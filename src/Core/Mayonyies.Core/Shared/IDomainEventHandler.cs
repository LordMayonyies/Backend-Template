namespace Mayonyies.Core.Shared;

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : DomainEventBase
{
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);
}