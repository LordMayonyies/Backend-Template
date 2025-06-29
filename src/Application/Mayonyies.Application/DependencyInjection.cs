using Mayonyies.Application.Authentication;
using Mayonyies.Application.Behaviors;
using Mayonyies.Application.DomainEvents;
using Mayonyies.Application.Jwt;
using Mayonyies.Core.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mayonyies.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommandsAndQueries();

        services.AddDomainEvents();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        
        services.AddTransient<IJwtService, JwtService>();

        services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }

    private static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();
        
        return services;
    }

    private static IServiceCollection AddCommandsAndQueries(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        
        services.Decorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandHandler<>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
        
        services.Decorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandHandler<>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
        services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));

        return services;
    }
}