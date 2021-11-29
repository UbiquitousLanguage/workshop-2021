using Eventuous;
using Eventuous.Subscriptions.Context;
using static Bookings.Integration.IntegrationEvents;
using EventHandler = Eventuous.Subscriptions.EventHandler;

namespace Bookings.Integration;

public class PaymentsIntegrationHandler : EventHandler {
    public static readonly StreamName Stream = new("PaymentsIntegration");

    public PaymentsIntegrationHandler() {
        On<BookingPaymentRecorded>(HandlePayment);
    }

    ValueTask HandlePayment(MessageConsumeContext<BookingPaymentRecorded> consumecontext) {
        throw new NotImplementedException();
    }
}

static class IntegrationEvents {
    [EventType("BookingPaymentRecorded")]
    public record BookingPaymentRecorded(string PaymentId, string BookingId, float Amount, string Currency);
}