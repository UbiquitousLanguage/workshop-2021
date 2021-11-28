using CoreLib.Mongo;
using MassTransit;
using Payments.Application;
using Payments.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoOptions = builder.Configuration.GetSection("Mongo").Get<MongoConfig.MongoOptions>();
builder.Services.AddSingleton(MongoConfig.ConfigureMongo(mongoOptions));
builder.Services.AddSingleton<IAggregateStore, MongoAggregateStore>();
builder.Services.AddSingleton<CommandService>();

builder.Services.AddSingleton<PublishEvent>(
    sp => (evt, ct) => sp.GetRequiredService<IPublishEndpoint>().Publish(evt, ct)
);

builder.AddBroker();

var app = builder.Build();

app.UseSwagger();
app.MapControllers();
app.UseSwaggerUI();

app.Run();