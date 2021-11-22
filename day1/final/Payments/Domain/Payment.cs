using CoreLib;
using static Payments.Domain.PaymentEvents;

namespace Payments.Domain;

public class Payment : Aggregate<PaymentId, PaymentState> {
    public void ProcessPayment(
        PaymentId paymentId, string bookingId, Money amount, string method, string provider
    )
        => Apply(new PaymentRecorded(paymentId.Value, bookingId, amount.Amount, amount.Currency, method, provider));

    protected override PaymentState When(object evt)
        => evt switch {
            PaymentRecorded recorded => State with {
                Id = recorded.PaymentId,
                BookingId = recorded.BookingId,
                Amount = recorded.Amount
            },
            _ => State
        };
}

public record PaymentState : AggregateState<PaymentId> {
    public string BookingId { get; init; } = null!;
    public float  Amount    { get; init; }
}

public record PaymentId(string Value) : AggregateId(Value);