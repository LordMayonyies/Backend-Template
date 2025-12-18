using Asp.Versioning;
using Mayonyies.Api.Authentication;
using Mayonyies.Api.HealthChecks;
using Mayonyies.Api.Logging;
using Mayonyies.Api.Versioning;
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
    .AddCustomApiVersioning()
    .AddCustomAuthentication(builder.Configuration)
    .AddAuthorization()
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

var app = builder.Build();

app.UseForwardedHeaders();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
    app.MapOpenApi("/openapi/{documentName}.yaml");

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

var version1Endpoints =
    app.MapGroup("v{version:apiVersion}")
        .WithApiVersionSet(apiVersionSet)
        .RequireAuthorization();

version1Endpoints
    .MapAuthentication();

await app.RunAsync();