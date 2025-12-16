using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mayonyies.Api.HealthChecks;

internal static class HealthChecksEndpoints
{
    internal static void MapHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/health")
            .DisableHttpMetrics();

        endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = registration => registration.Tags.Contains("live"),
            ResponseWriter = WriteHealthResponse
        })
        .DisableHttpMetrics();;

        endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = registration => registration.Tags.Contains("ready"),
            ResponseWriter = WriteHealthResponse
        })
        .DisableHttpMetrics();
    }

    private static Task WriteHealthResponse(HttpContext context, HealthReport report)
    {
        var payload =
            new
            {
                status = report.Status.ToString(),
                results = report.Entries.Select(entry =>
                    new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        description = entry.Value.Description,
                        duration = entry.Value.Duration.TotalMilliseconds
                    })
            };

        return context.Response.WriteAsJsonAsync(payload);
    }
}