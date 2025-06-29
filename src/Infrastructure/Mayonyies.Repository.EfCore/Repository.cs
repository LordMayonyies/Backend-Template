using System.Linq.Expressions;
using Mayonyies.Core.Shared;

namespace Mayonyies.Repository.EfCore;

internal abstract class Repository<T>(MayonyiesDbContext dbContext) : ReadRepository<T>(dbContext), IRepository<T>
    where T : Entity, IAggregateRoot
{
    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        return DbSet.AddAsync(entity, cancellationToken).AsTask();
    }

    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        return DbSet.AddRangeAsync(entities, cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);

        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        DbSet.RemoveRange(entities);
        
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return DbSet.Where(expression).ExecuteDeleteAsync(cancellationToken);
    }
}

internal abstract class Repository<TEntity, TPrimaryKey>(MayonyiesDbContext dbContext)
    : ReadRepository<TEntity, TPrimaryKey>(dbContext), IRepository<TEntity, TPrimaryKey>
    where TEntity : Entity<TPrimaryKey>, IAggregateRoot
    where TPrimaryKey : struct
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return DbSet.AddAsync(entity, cancellationToken).AsTask();
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return DbSet.AddRangeAsync(entities, cancellationToken);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);

        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbSet.RemoveRange(entities);
        
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return DbSet.Where(expression).ExecuteDeleteAsync(cancellationToken);
    }
}