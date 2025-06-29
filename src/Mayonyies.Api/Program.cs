using Mayonyies.Application;
using Mayonyies.Infrastructure;
using Mayonyies.Repository.EfCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSerilog(loggerConfiguration => 
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddInfrastructure();

builder.Services.AddEfCoreRepository(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.Run();