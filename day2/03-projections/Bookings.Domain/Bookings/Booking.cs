using Eventuous;
using static Bookings.Domain.Bookings.BookingEvents;
using static Bookings.Domain.Services;

namespace Bookings.Domain.Bookings;

public class Booking : Aggregate<BookingState, BookingId> {
    public async Task BookRoom(
        BookingId       bookingId,
        string          guestId,
        RoomId          roomId,
        StayPeriod      period,
        Money           price,
        Money           prepaid,
        DateTimeOffset  bookedAt,
        IsRoomAvailable isRoomAvailable
    ) {
        EnsureDoesntExist();
        await EnsureRoomAvailable(roomId, period, isRoomAvailable);

        var (amount, currency) = price - prepaid;

        Apply(
            new V1.RoomBooked(
                bookingId,
                guestId,
                roomId,
                period.CheckIn,
                period.CheckOut,
                price.Amount,
                prepaid.Amount,
                amount,
                currency,
                bookedAt
            )
        );

        MarkFullyPaidIfNecessary(bookedAt);
    }

    public void RecordPayment(Money paid, string paymentId, string paidBy, DateTimeOffset paidAt) {
        EnsureExists();

        if (State.HasPaymentBeenRecorded(paymentId)) return;

        var (amount, currency) = State.Outstanding - paid;

        Apply(
            new V1.PaymentRecorded(
                State.Id,
                paid.Amount,
                amount,
                currency,
                paymentId,
                paidBy,
                paidAt
            )
        );

        MarkFullyPaidIfNecessary(paidAt);
    }

    public void ApplyDiscount(Money discount, string discountCode, string appliedBy, DateTimeOffset appliedAt) {
        EnsureExists();
        
        if (State.HasUsedDiscountCode(discountCode)) return;
        
        var (amount, currency) = State.Outstanding - discount;

        Apply(
            new V1.DiscountApplied(
                State.Id,
                discount.Amount,
                amount,
                currency,
                discountCode,
                appliedBy,
                appliedAt
            )
        );
    }

    void MarkFullyPaidIfNecessary(DateTimeOffset when) {
        if (State.Outstanding.Amount != 0) return;

        Apply(new V1.BookingFullyPaid(State.Id, when));
    }

    static async Task EnsureRoomAvailable(RoomId roomId, StayPeriod period, IsRoomAvailable isRoomAvailable) {
        var roomAvailable = await isRoomAvailable(roomId, period);
        if (!roomAvailable) throw new DomainException("Room not available");
    }
}
