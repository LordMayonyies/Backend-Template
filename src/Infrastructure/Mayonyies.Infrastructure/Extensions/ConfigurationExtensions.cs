using Mayonyies.Application.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Mayonyies.Infrastructure.Extensions;

internal static class ConfigurationExtensions
{
    private const string DatabaseConnectionStringName = "MayonyiesDb";

    public static string GetConnectionString(this IConfiguration configuration) =>
        configuration.GetConnectionString(DatabaseConnectionStringName)
        ?? throw new ConfigurationException("You must configure a connection string for the database.");
}