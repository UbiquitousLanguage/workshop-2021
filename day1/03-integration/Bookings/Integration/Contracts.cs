// ReSharper disable CheckNamespace
namespace Payments.Integration;

public static class PaymentEvents {
    public record PaymentRecorded(
        string         PaymentId,
        string         BookingId,
        float          Amount,
        string         Currency,
        string         Method,
        string         Provider,
        DateTimeOffset PaidAt
    );
}
