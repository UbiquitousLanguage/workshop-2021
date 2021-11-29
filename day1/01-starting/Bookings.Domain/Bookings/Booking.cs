namespace Bookings.Domain.Bookings;

public class Booking : Aggregate<BookingId, BookingState> {
    public Booking()
        => State = new BookingState();
    public void BookRoom(
        BookingId         bookingId,
        string            guestId,
        RoomId            roomId,
        StayPeriod        period,
        Money             price
    ) {
        EnsureDoesntExist();

        ChangeState(new BookingState {
            Id          = bookingId.Value,
            RoomId      = roomId,
            GuestId     = guestId,
            Price       = price,
            Outstanding = price,
            Period      = period,
            Paid        = false
        });
    }

    public void AddPayment(Money paid) {
        ChangeState(State with {
            Payments = State.Payments.Add(paid)
        });
    }
}