using Mayonyies.Repository.EfCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mayonyies.Api.HealthChecks;

internal static class DependencyInjection
{
    internal static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), tags: ["live", "ready"])
            .AddDbContextCheck<MayonyiesDbContext>("database", tags: ["ready"]);

        return services;
    }
}