using Bookings.Application.Bookings;
using Bookings.Domain;
using Bookings.Infrastructure;
using CoreLib;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(
    MongoConfig.ConfigureMongo(
        builder.Configuration["MongoDb:ConnectionString"],
        builder.Configuration["MongoDb:Database"]
    )
);
builder.Services.AddSingleton<IAggregateStore, MongoAggregateStore>();

builder.Services
    .AddSingleton<BookingsCommandService>()
    .AddSingleton<Services.IsRoomAvailable>((id, period) => new ValueTask<bool>(true))
    .AddSingleton<Services.ConvertCurrency>((from, currency) => new Money(from.Amount * 2, currency));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();