using Bookings.Domain.Bookings;

namespace Bookings.Domain;

public static class Services {
    public delegate ValueTask<bool> IsRoomAvailable(
        RoomId roomId, StayPeriod period, CancellationToken cancellationToken
    );

    public delegate Money ConvertCurrency(Money from, string targetCurrency);

    public delegate bool CancellationPolicy(BookingState bookingState);
}