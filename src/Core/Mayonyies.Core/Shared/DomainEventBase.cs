namespace Mayonyies.Core.Shared;

/// <summary>
///     A base type for domain events. Depends on MediatR INotification.
///     Includes DateOccurred which is set on creation.
/// </summary>
public abstract class DomainEventBase
{
    public DateTime DateOccurredUtc { get; protected set; } = DateTime.UtcNow;
}