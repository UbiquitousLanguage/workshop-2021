namespace Bookings.Application.Bookings;

public static class BookingCommands {
    public record Book(
        string         BookingId,
        string         RoomId,
        string         GuestId,
        DateTime       From,
        DateTime       To,
        float          Price,
        string         Currency,
        string         BookedBy,
        DateTimeOffset BookedAt
    );
}