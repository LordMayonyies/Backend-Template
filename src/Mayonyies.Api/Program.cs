using Mayonyies.Api.Endpoints.Authentication;
using Mayonyies.Api.Authentication;
using Mayonyies.Api.HealthChecks;
using Mayonyies.Api.Logging;
using Mayonyies.Application;
using Mayonyies.Infrastructure;
using Mayonyies.Repository.EfCore;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

Log.Logger =
    new LoggerConfiguration()
        .WriteTo.Console()
        .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(loggerConfiguration =>
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddOpenApi();

builder.Services.Configure<ForwardedHeadersOptions>(options => { options.ForwardedHeaders = ForwardedHeaders.All; });

builder.Services
    .AddCustomHttpLogging()
    .AddCustomHealthChecks()
    ;

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddEfCoreRepository()
    .AddCommandsAndQueriesFromAssemblies(
        typeof(Mayonyies.Application.DependencyInjection),
        typeof(Mayonyies.Infrastructure.DependencyInjection),
        typeof(Mayonyies.Repository.EfCore.DependencyInjection),
        typeof(Mayonyies.Api.Extensions.IResultExtensions));

builder.Services
    .AddCustomAuthentication(builder.Configuration)
    .AddAuthorization();

var app = builder.Build();

app.UseForwardedHeaders();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks();

app.MapAuthentication();

await app.RunAsync();