using System.Linq.Expressions;

namespace Mayonyies.Core.Shared;

/// <summary>
///     <para>
///         A <see cref="IRepository{T}" /> can be used to query and save instances of <typeparamref name="T" />.
///     </para>
/// </summary>
/// <typeparam name="T">The type of entity being operated on by this repository.</typeparam>
public interface IRepository<T>
    where T : Entity, IAggregateRoot;

/// <summary>
///     <para>
///         A <see cref="IRepository{T,TId}" /> can be used to query and save instances of <typeparamref name="T" />.
///     </para>
/// </summary>
/// <typeparam name="T">The type of entity being operated on by this repository.</typeparam>
/// <typeparam name="TId">The type the entity uses as and identifier.</typeparam>
public interface IRepository<T, in TId> : IReadRepositoryBase<T, TId>
    where T : Entity<TId>, IAggregateRoot
    where TId : struct
{
    /// <summary>
    ///     Adds an entity in the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <typeparamref name="T" />.
    /// </returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds the given entities in the database
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    /// </returns>
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates an entity in the database
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates the given entities in the database
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Removes an entity in the database
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Removes the given entities in the database
    /// </summary>
    /// <param name="entities">The entities to remove.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Removes the all entities of <typeparamref name="T" />, that matches the <paramref name="expression" />, from the
    ///     database.
    /// </summary>
    /// <param name="expression">The expression used to filter the elements.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteRangeAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}