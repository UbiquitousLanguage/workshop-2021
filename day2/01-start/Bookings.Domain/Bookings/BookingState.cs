using Eventuous;
using static Bookings.Domain.Bookings.BookingEvents;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Bookings.Domain.Bookings;

public record BookingState : AggregateState<BookingState, BookingId> {
    public string     GuestId     { get; init; }
    public RoomId     RoomId      { get; init; }
    public StayPeriod Period      { get; init; }
    public Money      Price       { get; init; }
    public Money      Outstanding { get; init; }
    public bool       Paid        { get; init; }

    public BookingState() {
        On<V1.RoomBooked>(WhenBooked);
        On<V1.PaymentRecorded>(WhenPaymentRecorded);
        // On<V1.DiscountApplied>();
        On<V1.BookingFullyPaid>((state, paid) => state with { Paid = true });
    }

    static BookingState WhenBooked(BookingState state, V1.RoomBooked booked)
        => state with {
            Id = new BookingId(booked.BookingId),
            RoomId = new RoomId(booked.RoomId),
            Period = new StayPeriod(booked.CheckInDate, booked.CheckOutDate),
            GuestId = booked.GuestId,
            Price = new Money(booked.BookingPrice, booked.Currency),
            Outstanding = new Money(booked.BookingPrice, booked.Currency)
        };

    static BookingState WhenPaymentRecorded(BookingState state, V1.PaymentRecorded e)
        => state with {
            Outstanding = new Money(e.Outstanding, e.Currency),
        };
}
