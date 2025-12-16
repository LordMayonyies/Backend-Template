using Mayonyies.Core.Shared;
using Mayonyies.Core.Users;
using Mayonyies.Infrastructure.Extensions;
using Mayonyies.Repository.EfCore.Interceptors;
using Mayonyies.Repository.EfCore.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mayonyies.Repository.EfCore;

public static class DependencyInjection
{
    public static IServiceCollection AddEfCoreRepository(this IServiceCollection services)
    {
        services.AddSingleton<PublishDomainEventsSaveChangesInterceptor>();
        services.AddSingleton<UpdateAuditableEntitiesSaveChangesInterceptor>();

        services.AddDbContext<MayonyiesDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            options.UseNpgsql(configuration.GetConnectionString());

            options.AddInterceptors(
                serviceProvider.GetRequiredService<UpdateAuditableEntitiesSaveChangesInterceptor>(),
                serviceProvider.GetRequiredService<PublishDomainEventsSaveChangesInterceptor>()
            );
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MayonyiesDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}