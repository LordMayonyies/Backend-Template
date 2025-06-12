namespace Mayonyies.Core.Shared;

/// <summary>
///     A base type for domain events.
///     Includes DateOccurred which is set on creation.
/// </summary>
public abstract class DomainEventBase
{
    public DateTime DateOccurredUtc { get; } = DateTime.UtcNow;
}