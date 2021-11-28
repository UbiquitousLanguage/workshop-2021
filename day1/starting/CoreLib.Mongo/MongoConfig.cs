using MongoDB.Driver;

namespace CoreLib.Mongo;

public static class MongoConfig {
    public static IMongoDatabase ConfigureMongo(MongoOptions options) {
        var settings = MongoClientSettings.FromConnectionString(options.ConnectionString);
        return new MongoClient(settings).GetDatabase(options.Database);
    }

    public class MongoOptions {
        public string ConnectionString { get; set; } = null!;
        public string Database         { get; set; } = null!;
    }
}