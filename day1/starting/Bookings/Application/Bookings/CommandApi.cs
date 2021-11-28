using Microsoft.AspNetCore.Mvc;
using static Bookings.Application.Bookings.BookingCommands;

namespace Bookings.Application.Bookings;

[Route("/booking")]
public class CommandApi : ControllerBase {
    readonly BookingsCommandService _service;

    public CommandApi(BookingsCommandService service) => _service = service;

    [HttpPost]
    [Route("")]
    public Task DoIt([FromBody] Book cmd, CancellationToken cancellationToken)
        => _service.HandleNew(cmd, cancellationToken);
}