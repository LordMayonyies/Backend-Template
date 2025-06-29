using Mayonyies.Core.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Mayonyies.Repository.EfCore.Interceptors;

internal sealed class UpdateAuditableEntitiesSaveChangesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is not null)
            UpdateAuditableEntities(eventData.Context.ChangeTracker);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditableEntities(ChangeTracker changeTracker)
    {
        foreach (var entry in changeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(nameof(IAuditableEntity.CreatedAtUtc)).CurrentValue = DateTime.UtcNow;
                    entry.Property(nameof(IAuditableEntity.CreatedBy)).CurrentValue = "System"; // Replace with actual user context
                    break;
                case EntityState.Modified:
                    entry.Property(nameof(IAuditableEntity.ModifiedAtUtc)).CurrentValue = DateTime.UtcNow;
                    entry.Property(nameof(IAuditableEntity.ModifiedBy)).CurrentValue = "System"; // Replace with actual user context
                    break;
            }
        }
    }
}