using MassTransit;
using Payments.Integration;

namespace Bookings.Integration;

public class IntegrationConsumer : IConsumer<PaymentEvents.PaymentRecorded> {
    public Task Consume(ConsumeContext<PaymentEvents.PaymentRecorded> context) {
        throw new NotImplementedException();
    }
}