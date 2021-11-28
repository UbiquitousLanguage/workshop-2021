using Bookings.Domain;
using Bookings.Domain.Bookings;

namespace Bookings.Application.Bookings;

public class BookingsCommandService : CommandService<Booking, BookingId, BookingState> {
    public BookingsCommandService(
        IAggregateStore store, Services.IsRoomAvailable isRoomAvailable, Services.ConvertCurrency convertCurrency
    ) : base(store) {
        OnNew<BookingCommands.Book>(
            (booking, cmd) =>
                booking.BookRoom(
                    new BookingId(cmd.BookingId),
                    cmd.GuestId,
                    new RoomId(cmd.RoomId),
                    new StayPeriod(cmd.From, cmd.To),
                    new Money(cmd.Price, cmd.Currency),
                    cmd.BookedBy,
                    cmd.BookedAt,
                    isRoomAvailable
                )
        );

        OnExisting<BookingCommands.RecordPayment>(
            cmd => new BookingId(cmd.BookingId),
            (booking, cmd) => booking.RecordPayment(
                new Money(cmd.Amount, cmd.Currency),
                convertCurrency,
                cmd.PaidBy,
                cmd.PaidAt
            )
        );

        OnExisting<BookingCommands.ApplyDiscount>(
            cmd => new BookingId(cmd.BookingId),
            (booking, cmd) => booking.ApplyDiscount(
                new Money(cmd.Amount, cmd.Currency),
                convertCurrency
            )
        );
    }
}