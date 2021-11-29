using Eventuous;

namespace Bookings.Domain;

public record RoomId: AggregateId {
    public RoomId(string value) : base(value) { }
}