using Bookings.Application.Bookings;
using Bookings.Domain;
using CoreLib.Mongo;
using MongoDb.Bson.NodaTime;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

var builder = WebApplication.CreateBuilder(args);

NodaTimeSerializers.Register();
var mongoOptions = builder.Configuration.GetSection("Mongo").Get<MongoConfig.MongoOptions>();
builder.Services.AddSingleton(MongoConfig.ConfigureMongo(mongoOptions));
builder.Services.AddSingleton<IAggregateStore, MongoAggregateStore>();

builder.Services
    .AddSingleton<BookingsQueryService>()
    .AddSingleton<BookingsCommandService>()
    .AddSingleton<Services.IsRoomAvailable>((id,   period, _) => new ValueTask<bool>(true))
    .AddSingleton<Services.ConvertCurrency>((from, currency) => new Money(from.Amount * 2, currency));

builder.Services
    .AddControllers()
    .AddJsonOptions(cfg => cfg.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();