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