using MongoDb.Bson.NodaTime;
using MongoDB.Driver;

namespace Bookings.Infrastructure;

public static class Mongo {
    public static IMongoDatabase ConfigureMongo() {
        NodaTimeSerializers.Register();
        var settings = MongoClientSettings.FromConnectionString("mongodb://mongoadmin:secret@localhost:27017");
        return new MongoClient(settings).GetDatabase("bookings");
    }
}