using static Payments.Domain.PaymentEvents;

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
    ) {
        // Apply();
    }

    protected override PaymentState When(object evt)
        => evt switch {
            _ => State
        };
}

public record PaymentState : AggregateState<PaymentId> {
    public string BookingId { get; init; } = null!;
    public float  Amount    { get; init; }
}

public record PaymentId(string Value) : AggregateId(Value);