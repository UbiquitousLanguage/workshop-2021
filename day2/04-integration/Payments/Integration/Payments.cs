using Eventuous;
using Eventuous.Shovel;
using Eventuous.Subscriptions.Context;
using Payments.Domain;

namespace Payments.Integration;

public static class PaymentsShovel {
    static readonly StreamName Stream = new("PaymentsIntegration");
    
    public static ValueTask<ShovelContext?> Transform(IMessageConsumeContext original) {
        var result = original.Message is PaymentEvents.PaymentRecorded evt
            ? new ShovelContext(
                Stream,
                //
                new Metadata()
            )
            : null;
        return ValueTask.FromResult(result);
    }
}

public static class IntegrationEvents {
    [EventType("BookingPaymentRecorded")]
    public record BookingPaymentRecorded(string PaymentId, string BookingId, float Amount, string Currency);
}