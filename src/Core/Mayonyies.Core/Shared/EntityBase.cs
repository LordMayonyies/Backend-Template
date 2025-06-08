namespace Mayonyies.Core.Shared;

/// <summary>
///     A base class for DDD Entities. Includes support for domain events dispatched post-persistence.
///     If you prefer GUID Ids, change it here.
///     If you need to support an identifier with a type different from <see cref="int" />, change to
///     <see cref="EntityBase{TId}" /> and use TId as the type for Id.
/// </summary>
public abstract class EntityBase : EntityBase<int>
{
}

/// <summary>
///     A base class for DDD Entities. Includes support for domain events dispatched post-persistence.
///     If your {TId} it's of type <see cref="int" />, use <see cref="EntityBase" /> instead.
/// </summary>
public abstract class EntityBase<TId> : HasDomainEventsBase
    where TId : struct, IEquatable<TId>
{
    public TId Id { get; private init; } = default!;
}