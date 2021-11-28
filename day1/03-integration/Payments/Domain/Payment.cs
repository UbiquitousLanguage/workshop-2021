namespace Payments.Domain;

public class Payment : Aggregate<PaymentId, PaymentState> {
    public Payment() => State = new PaymentState();

    public void ProcessPayment(
        PaymentId      paymentId,
        string         bookingId,
        Money          amount,
        string         method,
        string         provider,
        DateTimeOffset paidAt
    )
        => State = new PaymentState {
            Id        = paymentId.Value,
            Amount    = amount,
            BookingId = bookingId
        };
}

public record PaymentState : AggregateState<PaymentId> {
    public string BookingId { get; init; } = null!;
    public Money  Amount    { get; init; } = null!;
}

public record PaymentId(string Value) : AggregateId(Value);