using Mayonyies.Api.Extensions;
using Mayonyies.Application;
using Mayonyies.Infrastructure;
using Mayonyies.Repository.EfCore;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

Log.Logger =
    new LoggerConfiguration()
        .WriteTo.Console()
        .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(loggerConfiguration =>
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddOpenApi();

builder.Services.AddHttpLogging(options =>
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

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddEfCoreRepository()
    .AddCommandsAndQueriesFromAssemblies(
        typeof(Mayonyies.Application.DependencyInjection),
        typeof(Mayonyies.Infrastructure.DependencyInjection),
        typeof(Mayonyies.Repository.EfCore.DependencyInjection),
        typeof(Mayonyies.Api.Extensions.ResultExtensions));

builder.Services
    .AddAuthentication(builder.Configuration)
    .AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.Run();
