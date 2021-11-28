using NodaTime;

namespace Bookings.Domain.Bookings;

public static class BookingEvents {
    public record RoomBooked(
        string         BookingId,
        string         RoomId,
        string         GuestId,
        LocalDate      CheckInDate,
        LocalDate      CheckOutDate,
        float          Price,
        string         Currency,
        string         BookedBy,
        DateTimeOffset BookedAt
    );
}