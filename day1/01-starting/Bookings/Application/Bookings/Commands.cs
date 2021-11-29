namespace Bookings.Application.Bookings;

public static class BookingCommands {
    public record BookRoom(
        string         BookingId,
        string         RoomId,
        string         GuestId,
        DateTime       From,
        DateTime       To,
        decimal        Price,
        string         Currency,
        string         BookedBy,
        DateTimeOffset BookedAt
    );

    public record RecordPayment(string BookingId, decimal Amount, string Currency);
}
