namespace Bookings.Domain;

public static class Services {
    public delegate ValueTask<bool> IsRoomAvailable(RoomId roomId, StayPeriod period);

    public delegate Money ConvertCurrency(Money from, string targetCurrency);

    public delegate ValueTask<Money> ApplyDiscount(Money original, string discountCode);
}