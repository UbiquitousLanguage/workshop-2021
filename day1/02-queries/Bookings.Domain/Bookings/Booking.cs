using static Bookings.Domain.Services;

namespace Bookings.Domain.Bookings;

public class Booking : Aggregate<BookingId, BookingState> {
    public Booking()
        => State = new BookingState();

    public async Task BookRoom(
        BookingId         bookingId,
        string            guestId,
        RoomId            roomId,
        StayPeriod        period,
        Money             price,
        string            bookedBy,
        DateTimeOffset    bookedAt,
        IsRoomAvailable   isRoomAvailable,
        CancellationToken cancellationToken
    ) {
        EnsureDoesntExist();
        await EnsureRoomAvailable(roomId, period, isRoomAvailable, cancellationToken);

        ChangeState(
            new BookingState {
                Id          = bookingId.Value,
                GuestId     = guestId,
                RoomId      = roomId,
                Price       = price,
                Period      = period,
                Outstanding = price,
                Paid        = price.Amount == 0
            }
        );
    }

    public void RecordPayment(
        Money           paid,
        ConvertCurrency convertCurrency,
        string          paidBy,
        DateTimeOffset  paidAt
    ) {
        EnsureExists();

        var localPaid = State.Price.IsSameCurrency(paid)
            ? paid
            : convertCurrency(paid, State.Price.Currency);
        var outstanding = State.Outstanding - localPaid;

        ChangeState(
            State with {
                Outstanding = outstanding,
                Paid = outstanding.Amount == 0
            }
        );
    }

    public void ApplyDiscount(Money discount, ConvertCurrency convertCurrency) {
        EnsureExists();

        var localDiscountAmount = State.Price.IsSameCurrency(discount)
            ? discount
            : convertCurrency(discount, State.Price.Currency);
        var outstanding = State.Outstanding - localDiscountAmount;

        ChangeState(
            State with {
                Outstanding = outstanding,
                Paid = outstanding.Amount == 0
            }
        );
    }

    public void CancelBooking(CancellationPolicy cancellationPolicy) {
        if (State.Cancelled) return;
        
        if (!cancellationPolicy(State)) throw new DomainException("Cannot cancel the booking");
        
        ChangeState(State with {Cancelled = true});
    }

    static async Task EnsureRoomAvailable(
        RoomId            roomId,
        StayPeriod        period,
        IsRoomAvailable   isRoomAvailable,
        CancellationToken cancellationToken
    ) {
        var roomAvailable = await isRoomAvailable(roomId, period, cancellationToken);
        if (!roomAvailable) throw new DomainException("Room not available");
    }
}
