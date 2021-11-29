using Eventuous;

namespace Bookings.Domain.Bookings;

public record BookingId : AggregateId {
    public BookingId(string value) : base(value) { }
}