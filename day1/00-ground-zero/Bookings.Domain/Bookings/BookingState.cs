namespace Bookings.Domain.Bookings;

public record BookingState : AggregateState<BookingId> {
    public string     GuestId     { get; set; }
    public RoomId     RoomId      { get; set; }
    public bool       Paid        { get; set; }
}