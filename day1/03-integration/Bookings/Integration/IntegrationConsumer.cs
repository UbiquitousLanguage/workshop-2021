using Bookings.Application.Bookings;
using MassTransit;
using Payments.Integration;

namespace Bookings.Integration;

public class IntegrationConsumer : IConsumer<PaymentEvents.PaymentRecorded> {
    readonly BookingsCommandService _service;

    public IntegrationConsumer(BookingsCommandService service)
        => _service = service;

    public Task Consume(ConsumeContext<PaymentEvents.PaymentRecorded> context)
        => _service.HandleExisting(
            new BookingCommands.RecordPayment(
                context.Message.BookingId,
                context.Message.PaymentId,
                context.Message.Amount,
                context.Message.Currency,
                context.Message.Provider,
                context.Message.PaidAt
            ),
            context.CancellationToken
        );
}