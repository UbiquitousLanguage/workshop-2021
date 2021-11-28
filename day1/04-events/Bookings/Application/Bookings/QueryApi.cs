using Microsoft.AspNetCore.Mvc;
using static Bookings.Application.Bookings.Queries;

namespace Bookings.Application.Bookings; 

[Route("/booking")]
public class QueryApi : ControllerBase {
    readonly BookingsQueryService _service;

    public QueryApi(BookingsQueryService service) => _service = service;

    [HttpGet]
    [Route("{id}")]
    public Task<GetBooking.Response> Get([FromRoute] string id, CancellationToken cancellationToken) 
        => _service.Handle(new GetBooking { Id = id }, cancellationToken);
}