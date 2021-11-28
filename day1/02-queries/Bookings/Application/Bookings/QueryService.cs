using Bookings.Domain.Bookings;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MongoTools;
using static Bookings.Application.Bookings.Queries;

namespace Bookings.Application.Bookings;

public class BookingsQueryService {
    readonly IMongoCollection<BookingState> _collection;

    public BookingsQueryService(IMongoDatabase database) {
        _collection = database.GetDocumentCollection<BookingState>();
    }

    public Task<GetBooking.Response> Handle(GetBooking query, CancellationToken cancellationToken)
        => _collection.LoadDocumentAs(
            Ensure.IsNotNullOrEmpty(query.Id, nameof(query.Id)),
            x => new GetBooking.Response {
                BookingId = x.Id,
                RoomId    = x.RoomId.Value,
                CheckIn   = x.Period.CheckIn,
                CheckOut  = x.Period.CheckOut,
                Paid      = x.Paid
            },
            cancellationToken
        );
}