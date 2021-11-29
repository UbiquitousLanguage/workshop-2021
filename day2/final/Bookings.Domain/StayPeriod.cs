using Eventuous;
using NodaTime;

namespace Bookings.Domain;

public record StayPeriod {
    public LocalDate CheckIn  { get; }
    public LocalDate CheckOut { get; }

    internal StayPeriod(LocalDate checkIn, LocalDate checkOut)
        => (CheckIn, CheckOut) = (checkIn, checkOut);

    public static StayPeriod FromDateTime(DateTime checkIn, DateTime checkOut) {
        var localCheckin  = LocalDate.FromDateTime(checkIn);
        var localCheckout = LocalDate.FromDateTime(checkOut);
        if (localCheckin >= localCheckout) 
            throw new DomainException("Check in date must be before check out date");

        return new StayPeriod(localCheckin, localCheckout);
    }
}