using System.Linq.Expressions;

namespace Mayonyies.Core.Shared;

/// <summary>
///     <para>
///         A <see cref="IReadRepositoryBase{T}" /> can be used to query instances of <typeparamref name="T" />.
///     </para>
/// </summary>
/// <typeparam name="T">The type of entity being operated on by this repository.</typeparam>
public interface IReadRepositoryBase<T>
    where T : EntityBase, IAggregateRoot;

/// <summary>
///     <para>
///         A <see cref="IReadRepositoryBase{T, TId}" /> can be used to query instances of <typeparamref name="T" />.
///     </para>
/// </summary>
/// <typeparam name="T">The type of entity being operated on by this repository.</typeparam>
/// <typeparam name="TId">The type the entity uses as and identifier.</typeparam>
public interface IReadRepositoryBase<T, in TId>
    where T : EntityBase<TId>, IAggregateRoot
    where TId : struct
{
    /// <summary>
    ///     Finds an entity with the given primary key value.
    /// </summary>
    /// <typeparam name="TId">The type of primary key.</typeparam>
    /// <param name="id">The value of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <typeparamref name="T" />, or <see langword="null" />.
    /// </returns>
    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the first element of a sequence, or a default value if the sequence contains no elements.
    /// </summary>
    /// <param name="expression">The expression used to find the element.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <typeparamref name="T" />, or <see langword="null" />.
    /// </returns>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an
    ///     exception if there is more than one element in the sequence.
    /// </summary>
    /// <param name="expression">The expression used to find the element.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <typeparamref name="T" />, or <see langword="null" />.
    /// </returns>
    Task<T?> SingleOrDefaultAsync(
        Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    ///     Finds all entities of <typeparamref name="T" /> from the database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<T>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds all entities of <typeparamref name="T" />, that matches the <paramref name="expression" />, from the
    ///     database.
    /// </summary>
    /// <param name="expression">The expression used to filter the elements.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns a number that represents how many entities satisfy the <paramref name="expression" />.
    /// </summary>
    /// <param name="expression">The expression used to filter the elements.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the
    ///     number of elements in the input sequence.
    /// </returns>
    Task<int> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the total number of records.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the
    ///     number of elements in the input sequence.
    /// </returns>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns a boolean that represents whether any entity satisfy the <paramref name="expression" /> or not.
    /// </summary>
    /// <param name="expression">The expression used to filter the elements.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains true if the
    ///     source sequence contains any elements; otherwise, false.
    /// </returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns a boolean whether any entity exists or not.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains true if the
    ///     source sequence contains any elements; otherwise, false.
    /// </returns>
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Finds all entities of <typeparamref name="T" />, that matches the <paramref name="expression" />, from the
    ///     database.
    /// </summary>
    /// <param name="expression">The encapsulated query logic.</param>
    /// <returns>
    ///     Returns an IAsyncEnumerable which can be enumerated asynchronously.
    /// </returns>
    IAsyncEnumerable<T> AsAsyncEnumerable(Expression<Func<T, bool>> expression);
}