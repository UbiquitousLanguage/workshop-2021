using Eventuous.Projections.MongoDB;
using MongoDB.Driver;
using static Bookings.Domain.Bookings.BookingEvents;

namespace Bookings.Application.Queries;

// public class MyBookingsProjection : MongoProjection<MyBookings> {
//     public MyBookingsProjection(IMongoDatabase database) : base(database)
//         => On<V1.RoomBooked>(
//         );
// }