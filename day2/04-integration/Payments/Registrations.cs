using Eventuous.EventStore;
using Eventuous.EventStore.Producers;
using Eventuous.EventStore.Subscriptions;
using Eventuous.Producers;
using Eventuous.Projections.MongoDB;
using Payments.Application;
using Payments.Domain;
using Payments.Infrastructure;
using Payments.Integration;

namespace Payments; 

public static class Registrations {
    public static void AddServices(this IServiceCollection services) {
        services.AddEventStoreClient("esdb://localhost:2113?tls=false");
        services.AddAggregateStore<EsdbEventStore>();
        services.AddApplicationService<CommandService, Payment>();
        services.AddSingleton(Mongo.ConfigureMongo());
        services.AddCheckpointStore<MongoCheckpointStore>();
        services.AddEventProducer<EventStoreProducer>();

        services
            .AddShovel<AllStreamSubscription, AllStreamSubscriptionOptions, EventStoreProducer>(
                "IntegrationSubscription",
                PaymentsShovel.Transform
            );
    }
}