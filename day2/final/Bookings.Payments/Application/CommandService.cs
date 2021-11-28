using Bookings.Payments.Domain;
using Eventuous;
using Eventuous.AspNetCore.Web;

namespace Bookings.Payments.Application;

public class CommandService : ApplicationService<Payment, PaymentState, PaymentId> {
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

[AggregateCommands(typeof(Payment))]
public static class PaymentCommands {
    [HttpCommand]
    public record RecordPayment(string PaymentId, string BookingId, float Amount, string Currency, string Method, string Provider);
}