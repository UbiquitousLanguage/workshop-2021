namespace Bookings.Application;

public static class Commands {
    public record BookRoom(
        string         BookingId,
        string         RoomId,
        string         GuestId,
        decimal        Amount,
        string         Currency,
        DateTime       CheckIn,
        DateTime       CheckOut,
        DateTimeOffset BookedAt
    );
}
