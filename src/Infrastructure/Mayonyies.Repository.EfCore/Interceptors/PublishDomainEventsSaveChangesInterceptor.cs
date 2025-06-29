using Mayonyies.Application.DomainEvents;
using Mayonyies.Core.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Mayonyies.Repository.EfCore.Interceptors;

internal sealed class PublishDomainEventsSaveChangesInterceptor(IDomainEventsDispatcher domainEventsDispatcher)
    : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is not null)
            await PublishDomainEventsAsync(eventData.Context.ChangeTracker, cancellationToken);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEventsAsync(
        ChangeTracker changeTracker,
        CancellationToken cancellationToken = default)
    {
        var domainEvents = changeTracker
            .Entries<HasDomainEventsBase>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => entity.GetAndClearDomainEvents());

        await domainEventsDispatcher.DispatchAsync(domainEvents, cancellationToken);
    }
}