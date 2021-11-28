using Payments.Domain;
using static Payments.Application.PaymentCommands;

namespace Payments.Application;

public delegate Task PublishEvent(object evt, CancellationToken cancellationToken);

public class CommandService : CommandService<Payment, PaymentId, PaymentState> {
    public CommandService(IAggregateStore store, PublishEvent publish) : base(store) {
        OnNewAsync<RecordPayment>(
            async (payment, cmd, ct) => {
                payment.ProcessPayment(
                    new PaymentId(cmd.PaymentId),
                    cmd.BookingId,
                    new Money(cmd.Amount, cmd.Currency),
                    cmd.Method,
                    cmd.Provider,
                    DateTimeOffset.UtcNow
                );

                // await publish();
            }
        );
    }
}

public static class PaymentCommands {
    public record RecordPayment(
        string PaymentId,
        string BookingId,
        float  Amount,
        string Currency,
        string Method,
        string Provider
    );
}