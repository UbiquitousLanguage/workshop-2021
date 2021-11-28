namespace Payments.Domain;

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