using Payments;
using Payments.Infrastructure;
using Eventuous.AspNetCore;
using Serilog;

Logging.ConfigureLog();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();
builder.Host.UseSerilog();

var app = builder.Build();
app.AddEventuousLogs();

app.UseSwagger();

// Here we discover commands by their annotations
app.MapDiscoveredCommands();

app.UseSwaggerUI();

app.Run();