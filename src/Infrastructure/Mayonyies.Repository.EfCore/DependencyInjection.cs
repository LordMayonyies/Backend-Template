using Mayonyies.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mayonyies.Repository.EfCore;

public static class DependencyInjection
{
    public static IServiceCollection AddEfCoreRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MayonyiesDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString());
        });

        return services;
    }
}