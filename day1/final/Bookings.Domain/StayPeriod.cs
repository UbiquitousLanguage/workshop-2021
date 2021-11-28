using NodaTime;

namespace Bookings.Domain; 

public record StayPeriod {
    public LocalDate CheckIn  { get; internal init; }
    public LocalDate CheckOut { get; internal init; }
        
    internal StayPeriod() { }

    public static StayPeriod FromDateTime(DateTime checkIn, DateTime checkOut) {
        if (checkIn > checkOut) throw new DomainException("Check in date must be before check out date");

        return new StayPeriod {
            CheckIn = LocalDate.FromDateTime(checkIn),
            CheckOut =  LocalDate.FromDateTime(checkOut)
        };
    }
}