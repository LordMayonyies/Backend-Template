using Mayonyies.Application.DomainEvents;
using Mayonyies.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace Mayonyies.Repository.EfCore;

internal sealed class MayonyiesDbContext(
    DbContextOptions<MayonyiesDbContext> options,
    IDomainEventsDispatcher domainEventsDispatcher)
    : DbContext(options ), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MayonyiesDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        
        await PublishDomainEventsAsync(cancellationToken);

        return result;
    }

    private async Task PublishDomainEventsAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker
            .Entries<HasDomainEventsBase>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => entity.GetAndClearDomainEvents());
        
        await domainEventsDispatcher.DispatchAsync(domainEvents, cancellationToken);
    }
}