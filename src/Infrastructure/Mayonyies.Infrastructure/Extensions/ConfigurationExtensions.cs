using Mayonyies.Application.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Mayonyies.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    private const string DatabaseConnectionStringName = "Database";

    public static string GetConnectionString(this IConfiguration configuration) =>
        configuration.GetConnectionString("Database")
        ?? throw new ConfigurationException("You must configure a connection string for the database.");
}