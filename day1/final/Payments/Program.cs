using CoreLib.Mongo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoOptions = builder.Configuration.GetSection("Mongo").Get<MongoConfig.MongoOptions>();
builder.Services.AddSingleton(MongoConfig.ConfigureMongo(mongoOptions));
builder.Services.AddSingleton<IAggregateStore, MongoAggregateStore>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();