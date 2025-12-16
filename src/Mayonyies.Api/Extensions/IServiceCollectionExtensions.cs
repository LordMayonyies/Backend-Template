using System.Text;
using Mayonyies.Application.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Mayonyies.Api.Extensions;

internal static class IServiceCollectionExtensions
{
    internal static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration[$"{nameof(JwtOptions.SectionName)}:{nameof(JwtOptions.Issuer)}"],
                    ValidAudience = configuration[$"{nameof(JwtOptions.SectionName)}:{nameof(JwtOptions.Audience)}"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                configuration[$"{nameof(JwtOptions.SectionName)}:{nameof(JwtOptions.SecretKey)}"]!
                            )
                        )
                };
            });

        return services;
    }
}