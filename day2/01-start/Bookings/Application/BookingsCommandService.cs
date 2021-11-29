using Bookings.Domain;
using Bookings.Domain.Bookings;
using Eventuous;
using static Bookings.Application.BookingCommands;

namespace Bookings.Application;

public class BookingsCommandService : ApplicationService<Booking, BookingState, BookingId> {
    public BookingsCommandService(
        IAggregateStore          store,
        Services.IsRoomAvailable isRoomAvailable,
        Services.ApplyDiscount   applyDiscount
    ) : base(store) {
        OnNewAsync<BookRoom>(
            (booking, cmd, _) => booking.BookRoom(
                new BookingId(cmd.BookingId),
                cmd.GuestId,
                new RoomId(cmd.RoomId),
                StayPeriod.FromDateTime(cmd.CheckInDate, cmd.CheckOutDate),
                Money.FromCurrency(cmd.BookingPrice, cmd.Currency),
                Money.FromCurrency(cmd.PrepaidAmount, cmd.Currency),
                DateTimeOffset.Now,
                isRoomAvailable
            )
        );

        OnExistingAsync<ApplyDiscount>(
            cmd => new BookingId(cmd.BookingId),
            async (booking, cmd, _) => {
                var discount = await applyDiscount(booking.State.Price, cmd.DiscountCode);
            }
        );

        OnExisting<RecordPayment>(
            cmd => new BookingId(cmd.BookingId),
            (booking, cmd) => booking.RecordPayment(
                Money.FromCurrency(cmd.PaidAmount, cmd.Currency),
                cmd.PaymentId,
                cmd.PaidBy,
                DateTimeOffset.Now
            )
        );
    }
}
