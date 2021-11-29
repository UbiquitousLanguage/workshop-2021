using Microsoft.AspNetCore.Mvc;
using static Bookings.Application.Commands;

namespace Bookings.Application;

[Route("/booking")]
public class BookingCommandApi : ControllerBase {
    readonly CommandService _service;

    public BookingCommandApi(CommandService service) => _service = service;

    [HttpPost]
    [Route("book")]
    public Task BookRoom([FromBody] BookRoom cmd, CancellationToken cancellationToken)
        => _service.HandleNew(cmd, cancellationToken);
}