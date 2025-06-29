namespace Mayonyies.Core.Shared;

/// <summary>
///     A base class for DDD Entities. Includes support for domain events dispatched post-persistence.
///     If you want a default implementation of the EntityBase with a single Id, change to
///     <see cref="Entity{TId}" /> and use TId as the type for Id.
/// </summary>
public abstract class Entity : HasDomainEventsBase, IAuditableEntity, IEquatable<Entity>
{
    public const int IdTextMaxLength = 50;
    public const int ShortTextMaxLength = 100;
    public const int TextMaxLength = 500;
    public const int LongTextMaxLength = 1000;

    public DateTime CreatedAtUtc { get; private set; }
    public string CreatedBy { get; private set; } = null!;
    public DateTime ModifiedAtUtc { get; private set; }
    public string ModifiedBy { get; private set; } = null!;

    public abstract bool Equals(Entity? entityBase);

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity);
    }

    public abstract override int GetHashCode();

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right) =>
        !(left == right);
}

/// <summary>
///     A base class for DDD Entities. Includes support for domain events dispatched post-persistence.
///     If your {TId} it's of type <see cref="int" />, use <see cref="Entity" /> instead.
/// </summary>
public abstract class Entity<TId> : Entity, IAuditableEntity, IEquatable<Entity<TId>>
    where TId : struct
{
    public TId Id { get; private init; } = default!;

    public override bool Equals(Entity? entityBase) =>
        Equals(entityBase as Entity<TId>);

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return GetType() == other.GetType() && EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj) =>
        Equals(obj as Entity<TId>);

    public override int GetHashCode() =>
        HashCode.Combine(GetType(), Id);
}