using Bookings.Domain;
using Bookings.Domain.Bookings;
using static Bookings.Application.Bookings.BookingCommands;

namespace Bookings.Application.Bookings;

public class BookingsCommandService : CommandService<Booking, BookingId, BookingState> {
    public BookingsCommandService(IAggregateStore store) : base(store) {
        OnNew<BookRoom>(
            (booking, cmd) =>
                booking.BookRoom(
                    new BookingId(cmd.BookingId),
                    cmd.GuestId,
                    new RoomId(cmd.RoomId),
                    StayPeriod.FromDateTime(cmd.From, cmd.To),
                    new Money(cmd.Price, cmd.Currency)
                )
        );
        
        OnExisting<RecordPayment>(
            cmd => new BookingId(cmd.BookingId),
            (booking, cmd) => booking.AddPayment(new Money(cmd.Amount, cmd.Currency))
            );
    }
}