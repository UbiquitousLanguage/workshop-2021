using Bookings.Domain.Bookings;

namespace Bookings.Application;

public class CommandService : CommandService<Booking, BookingId, BookingState> {
    public CommandService(IAggregateStore store) : base(store) {
        OnNew<SomeCommand>(
            (booking, cmd) =>
        );
    }
}