using MongoDB.Driver;

namespace Bookings.Infrastructure;

static class MongoConfig {
    public static IMongoDatabase ConfigureMongo(string connectionString, string database) {
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        return new MongoClient(settings).GetDatabase(database);
    }
}