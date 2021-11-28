using Bookings.Application.Bookings;
using Bookings.Domain;
using Bookings.Infrastructure;
using Bookings.Integration;
using CoreLib.Mongo;

var builder = WebApplication.CreateBuilder(args);

var mongoOptions = builder.Configuration.GetSection("Mongo").Get<MongoConfig.MongoOptions>();
builder.Services.AddSingleton(MongoConfig.ConfigureMongo(mongoOptions));
builder.Services.AddSingleton<IAggregateStore, MongoAggregateStore>();

builder.Services
    .AddSingleton<BookingsCommandService>()
    .AddSingleton<Services.IsRoomAvailable>((id, period, _) => new ValueTask<bool>(true))
    .AddSingleton<Services.ConvertCurrency>((from, currency) => new Money(from.Amount * 2, currency));

builder.AddBroker(x => x.AddConsumer<IntegrationConsumer>());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();