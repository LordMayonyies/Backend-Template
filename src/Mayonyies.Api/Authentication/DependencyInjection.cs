using System.Text;
using Mayonyies.Application.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Mayonyies.Api.Authentication;

internal static class DependencyInjection
{
    internal static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration[$"{JwtOptions.SectionName}:{nameof(JwtOptions.Issuer)}"],
                    ValidAudience = configuration[$"{JwtOptions.SectionName}:{nameof(JwtOptions.Audience)}"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                configuration[$"{JwtOptions.SectionName}:{nameof(JwtOptions.SecretKey)}"]!
                            )
                        )
                };
            });

        return services;
    }
}