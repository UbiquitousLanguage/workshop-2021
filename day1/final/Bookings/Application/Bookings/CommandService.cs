using Bookings.Domain;
using Bookings.Domain.Bookings;
using static Bookings.Application.Bookings.BookingCommands;

namespace Bookings.Application.Bookings;

public class BookingsCommandService : CommandService<Booking, BookingId, BookingState> {
    public BookingsCommandService(
        IAggregateStore store, Services.IsRoomAvailable isRoomAvailable, Services.ConvertCurrency convertCurrency
    ) : base(store) {
        OnNewAsync<Book>(
            async (booking, cmd, ct) =>
                await booking.BookRoom(
                    new BookingId(cmd.BookingId),
                    cmd.GuestId,
                    new RoomId(cmd.RoomId),
                    new StayPeriod(cmd.From, cmd.To),
                    new Money(cmd.Price, cmd.Currency),
                    cmd.BookedBy,
                    cmd.BookedAt,
                    isRoomAvailable,
                    ct
                )
        );

        OnExisting<RecordPayment>(
            cmd => new BookingId(cmd.BookingId),
            (booking, cmd) => booking.RecordPayment(
                new Money(cmd.Amount, cmd.Currency),
                convertCurrency,
                cmd.PaidBy,
                cmd.PaidAt
            )
        );

        OnExisting<ApplyDiscount>(
            cmd => new BookingId(cmd.BookingId),
            (booking, cmd) => booking.ApplyDiscount(
                new Money(cmd.Amount, cmd.Currency),
                convertCurrency
            )
        );
    }
}