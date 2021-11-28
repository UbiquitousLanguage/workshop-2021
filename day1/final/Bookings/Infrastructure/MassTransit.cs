using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace Bookings.Infrastructure;

static class MassTransit {
    public static void AddBroker(this WebApplicationBuilder builder, Action<IServiceCollectionBusConfigurator> configure) {
        builder.Services.AddMassTransit(
            x => {
                configure(x);
                x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
            }
        );
        builder.Services.AddMassTransitHostedService();
    }
}