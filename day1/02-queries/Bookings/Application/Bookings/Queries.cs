using NodaTime;

namespace Bookings.Application.Bookings;

public static class Queries {
    public class GetBooking {
        public string? Id { get; set; }
        
        public class Response {
            public string    BookingId { get; set; } = null!;
            public LocalDate CheckIn   { get; set; }
            public LocalDate CheckOut  { get; set; }
            public string?   RoomId    { get; init; }
            public bool      Paid      { get; set; }
        }
    }
}