using CoreLib;
using Payments.Domain;

namespace Payments.Application;

public class CommandService : CommandService<Payment, PaymentId, PaymentState> {
    public CommandService(IAggregateStore store) : base(store) {
        OnNew<PaymentCommands.RecordPayment>(
            (payment, cmd) => payment.ProcessPayment(
                new PaymentId(cmd.PaymentId),
                cmd.BookingId,
                new Money(cmd.Amount, cmd.Currency),
                cmd.Method, 
                cmd.Provider
            )
        );
    }
}

public static class PaymentCommands {
    public record RecordPayment(string PaymentId, string BookingId, float Amount, string Currency, string Method, string Provider);
}