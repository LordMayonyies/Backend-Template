using System.Linq.Expressions;
using Mayonyies.Core.Shared;

namespace Mayonyies.Repository.EfCore;

internal abstract class ReadRepository<T>(MayonyiesDbContext dbContext) : IReadRepository<T>
    where T : Entity, IAggregateRoot
{
    protected readonly MayonyiesDbContext DbContext = dbContext;
    protected readonly DbSet<T> DbSet = dbContext.Set<T>();

    public Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default) =>
        DbSet.FirstOrDefaultAsync(expression, cancellationToken);

    public Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default) =>
        DbSet.SingleOrDefaultAsync(expression, cancellationToken);

    public Task<List<T>> ListAsync(CancellationToken cancellationToken = default) =>
        DbSet.ToListAsync(cancellationToken);

    public Task<List<T>> ListAsync(
        Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default
    ) =>
        DbSet.Where(expression).ToListAsync(cancellationToken);

    public Task<int> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) =>
        DbSet.CountAsync(expression, cancellationToken);

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        DbSet.CountAsync(cancellationToken);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) =>
        DbSet.AnyAsync(expression, cancellationToken);

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        DbSet.AnyAsync(cancellationToken);

    public IAsyncEnumerable<T> AsAsyncEnumerable(Expression<Func<T, bool>> expression) =>
        DbSet.Where(expression).AsAsyncEnumerable();
}

internal abstract class ReadRepository<TEntity, TPrimaryKey>(MayonyiesDbContext dbContext)
    : ReadRepository<TEntity>(dbContext),
        IReadRepository<TEntity, TPrimaryKey>
    where TEntity : Entity<TPrimaryKey>, IAggregateRoot
    where TPrimaryKey : struct
{
    public Task<TEntity?> GetByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default) =>
        DbSet.FindAsync([id], cancellationToken).AsTask();
}