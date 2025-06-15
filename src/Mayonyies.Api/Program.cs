using Mayonyies.Application;
using Mayonyies.Infrastructure;
using Mayonyies.Repository.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplication();

builder.Services.AddInfrastructure();

builder.Services.AddEfCoreRepository(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.Run();