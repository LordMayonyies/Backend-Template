using Mayonyies.Core.Shared;

namespace Mayonyies.Repository.EfCore;

internal sealed class MayonyiesDbContext(
    DbContextOptions<MayonyiesDbContext> options
)
    : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MayonyiesDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}