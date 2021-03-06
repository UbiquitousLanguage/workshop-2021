using MassTransit;

namespace Payments.Infrastructure; 

static class MassTransit {
    public static void AddBroker(this WebApplicationBuilder builder) {
        builder.Services.AddMassTransit(x => x.UsingRabbitMq());
        builder.Services.AddMassTransitHostedService();
    }
}