using Bookings.Application.Bookings;
using MassTransit;
using static Payments.Domain.PaymentEvents;

namespace Bookings.Integration;

public class IntegrationConsumer : IConsumer<PaymentRecorded> {
    readonly BookingsCommandService _service;
    
    public IntegrationConsumer(BookingsCommandService service) => _service = service;

    public Task Consume(ConsumeContext<PaymentRecorded> context) {
        var cmd = new BookingCommands.RecordPayment(
            context.Message.BookingId,
            context.Message.PaymentId,
            context.Message.Amount,
            context.Message.Currency,
            context.Message.Provider,
            context.Message.PaidAt
        );
        return _service.HandleExisting(cmd, context.CancellationToken);
    }
}