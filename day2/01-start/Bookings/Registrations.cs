using System.Text.Json;
using Bookings.Application;
using Bookings.Domain;
using Bookings.Domain.Bookings;
using Eventuous;
using Eventuous.EventStore;
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
    }
}