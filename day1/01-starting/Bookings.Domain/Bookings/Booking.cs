namespace Bookings.Domain.Bookings;

public class Booking : Aggregate<BookingId, BookingState> {
    public void BookRoom(
        BookingId         bookingId,
        string            guestId,
        RoomId            roomId,
        StayPeriod        period,
        Money             price
    ) {
        EnsureDoesntExist();

        State = new BookingState {
            Id          = bookingId.Value,
            RoomId      = roomId,
            GuestId     = guestId,
            Price       = price,
            Outstanding = price,
            Period      = period,
            Paid        = false
        };
    }
}