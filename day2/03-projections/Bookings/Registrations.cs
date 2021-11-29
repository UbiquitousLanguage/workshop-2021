using System.Text.Json;
using Bookings.Application;
using Bookings.Application.Queries;
using Bookings.Domain;
using Bookings.Domain.Bookings;
using Bookings.Infrastructure;
using Bookings.Integration;
using Eventuous;
using Eventuous.EventStore;
using Eventuous.EventStore.Subscriptions;
using Eventuous.Projections.MongoDB;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace Bookings;

public static class Registrations {
    public static void AddEventuous(this IServiceCollection services) {
        DefaultEventSerializer.SetDefaultSerializer(
            new DefaultEventSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web).ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
            )
        );

        services.AddEventStoreClient("esdb://localhost:2113?tls=false");
        services.AddAggregateStore<EsdbEventStore>();
        services.AddApplicationService<BookingsCommandService, Booking>();

        services.AddSingleton<Services.IsRoomAvailable>((id, period) => new ValueTask<bool>(true));
        services.AddSingleton<Services.ApplyDiscount>(DiscountService.GetDiscount);
        services.AddSingleton<Services.ConvertCurrency>((from, currency) => Money.FromCurrency(from.Amount * 2, currency));

        services.AddSingleton(Mongo.ConfigureMongo());
        services.AddCheckpointStore<MongoCheckpointStore>();

        services.AddSubscription<AllStreamSubscription, AllStreamSubscriptionOptions>(
            "BookingsProjections",
            builder => builder
                .AddEventHandler<BookingStateProjection>()
                // .AddEventHandler<MyBookingsProjection>()
        );

        services.AddSubscription<StreamSubscription, StreamSubscriptionOptions>(
            "PaymentIntegration",
            builder => builder
                .Configure(x => x.StreamName = PaymentsIntegrationHandler.Stream)
                .AddEventHandler<PaymentsIntegrationHandler>()
        );
    }
}