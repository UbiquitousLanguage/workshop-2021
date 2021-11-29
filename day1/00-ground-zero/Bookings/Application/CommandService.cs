using Bookings.Domain;
using Bookings.Domain.Bookings;

namespace Bookings.Application;

public class CommandService : CommandService<Booking, BookingId, BookingState> {
    public CommandService(IAggregateStore store) : base(store) {
        OnNew<Commands.BookRoom>(
            (booking, cmd) => {
                booking.BookRoom(
                    new BookingId(cmd.BookingId),
                    cmd.GuestId,
                    new RoomId(cmd.RoomId),
                    StayPeriod.FromDateTime(cmd.CheckIn, cmd.CheckOut)
                );
            }
        );
    }
}
