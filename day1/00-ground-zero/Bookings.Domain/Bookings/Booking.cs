namespace Bookings.Domain.Bookings;

public class Booking : Aggregate<BookingId, BookingState> {
    public void BookRoom(
        BookingId         bookingId,
        string            guestId,
        RoomId            roomId
    ) {
        EnsureDoesntExist();

        State = new BookingState {
            Id          = bookingId.Value,
            RoomId      = roomId,
            GuestId     = guestId,
            Paid        = false
        };
    }
}