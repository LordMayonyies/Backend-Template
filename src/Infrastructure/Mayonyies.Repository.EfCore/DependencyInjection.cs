using Mayonyies.Core.Shared;
using Mayonyies.Core.Users;
using Mayonyies.Infrastructure.Extensions;
using Mayonyies.Repository.EfCore.Interceptors;
using Mayonyies.Repository.EfCore.Users;
using Microsoft.AspNetCore.Identity;
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

            options.UseAsyncSeeding((_, _, cancellationToken) => SeedDataAsync(serviceProvider, cancellationToken));
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MayonyiesDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static async Task SeedDataAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var needsToSaveChanges = false;

        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var adminUser = await userRepository.SingleOrDefaultAsync(user => user.Username == "admin", cancellationToken);

        if (adminUser is null)
        {
            needsToSaveChanges = true;

            adminUser = new User("admin", "admin@mayonyies.com");

            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();

            adminUser.SetPasswordHash(passwordHasher.HashPassword(adminUser, "admin"));

            await userRepository.AddAsync(adminUser, cancellationToken);
        }

        if (needsToSaveChanges)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}