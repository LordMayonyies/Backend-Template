using System.Collections.Concurrent;
using System.Reflection;
using Mayonyies.Core.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Mayonyies.Application.DomainEvents;

internal sealed class DomainEventsDispatcher(IServiceProvider serviceProvider) : IDomainEventsDispatcher
{
    private static readonly ConcurrentDictionary<Type, Type> HandlerTypeCache = new();
    private static readonly ConcurrentDictionary<Type, MethodInfo?> HandleAsyncMethodCache = new();

    public async Task DispatchAsync(
        IEnumerable<DomainEventBase> domainEvents,
        CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            using var scope = serviceProvider.CreateScope();

            var handlerType =
                HandlerTypeCache.GetOrAdd(
                    domainEvent.GetType(),
                    domainEventType => typeof(IDomainEventHandler<>).MakeGenericType(domainEventType));

            var handlers = scope.ServiceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                if (handler is null)
                    continue;
                
                var handleAsyncMethod = HandleAsyncMethodCache.GetOrAdd(
                    handlerType,
                    ht => ht.GetMethod(nameof(IDomainEventHandler<>.HandleAsync)));

                if (handleAsyncMethod is not null)
                    await (Task)handleAsyncMethod.Invoke(handler, [domainEvents, cancellationToken])!;
            }
        }
    }
}