using Asp.Versioning;

namespace Mayonyies.Api.Versioning;

public static class ApiVersioningExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCustomApiVersioning()
        {
            services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1);
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader(),
                        new HeaderApiVersionReader()
                    );
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'V";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }
    }
}