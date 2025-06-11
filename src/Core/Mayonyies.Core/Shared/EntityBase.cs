namespace Mayonyies.Core.Shared;

public interface IAuditableEntity
{
    /// <summary>
    ///     The date and time when the entity was created.
    /// </summary>
    DateTime CreatedAtUtc { get; }
    
    /// <summary>
    ///     The user or system that created the entity.
    /// </summary>
    string CreatedBy { get; }

    /// <summary>
    ///     The date and time when the entity was last modified.
    /// </summary>
    DateTime? ModifiedAtUct { get; }
    
    /// <summary>
    ///     The user or system that last modified the entity.
    /// </summary>
    string ModifiedBy { get; }
}

/// <summary>
///     A base class for DDD Entities. Includes support for domain events dispatched post-persistence.
///     If you want a default implementation of the EntityBase with a single Id, change to
///     <see cref="EntityBase{TId}" /> and use TId as the type for Id.
/// </summary>
public abstract class EntityBase : HasDomainEventsBase, IAuditableEntity, IEquatable<EntityBase>
{
    public DateTime CreatedAtUtc { get; }
    public string CreatedBy { get; }
    public DateTime? ModifiedAtUct { get; }
    public string ModifiedBy { get; }

    public abstract bool Equals(EntityBase? other);

    public override bool Equals(object? obj)
    {
        return Equals(obj as EntityBase);
    }

    public abstract override int GetHashCode();
}

/// <summary>
///     A base class for DDD Entities. Includes support for domain events dispatched post-persistence.
///     If your {TId} it's of type <see cref="int" />, use <see cref="EntityBase" /> instead.
/// </summary>
public abstract class EntityBase<TId> : EntityBase
    where TId : struct
{
    public TId Id { get; private init; } = default!;
    
    public override bool Equals(EntityBase? entityBase)
    {
        if (entityBase is null) 
            return false;
        
        if (ReferenceEquals(this, entityBase)) 
            return true;
        
        if (GetType() != entityBase.GetType()) 
            return false;

        return entityBase is EntityBase<TId> other && Id.Equals(other.Id);
    }
}