using Microsoft.AspNetCore.HttpLogging;

namespace Mayonyies.Api.Logging;

internal static class DependencyInjection
{
    internal static IServiceCollection AddCustomHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(options =>
        {
            options.LoggingFields =
                HttpLoggingFields.RequestPropertiesAndHeaders |
                HttpLoggingFields.RequestQuery |
                HttpLoggingFields.RequestBody |
                HttpLoggingFields.ResponsePropertiesAndHeaders |
                HttpLoggingFields.ResponseStatusCode |
                HttpLoggingFields.ResponseBody;

            // options.RequestHeaders.Add(HeaderDictionaryExtensions.ClientVersionHeaderKey);
            // options.ResponseHeaders.Add(PaginationMetadata.HeaderKey);
            options.RequestHeaders.Add("X-Forwarded-For");
            options.RequestHeaders.Add("X-Forwarded-Proto");
            options.RequestHeaders.Add("X-Forwarded-Host");
            options.RequestHeaders.Add("X-Forwarded-Prefix");

            options.RequestBodyLogLimit = 1000000;
            options.ResponseBodyLogLimit = 1000000;
        });
        
        return services;
    }
}