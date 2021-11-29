using Microsoft.AspNetCore.Mvc;
using static Bookings.Application.Bookings.BookingCommands;

namespace Bookings.Application.Bookings;

[Route("/booking")]
public class CommandApi : ControllerBase {
    readonly BookingsCommandService _service;

    public CommandApi(BookingsCommandService service) => _service = service;

    [HttpPost]
    [Route("book")]
    public Task BookRoom([FromBody] BookRoom cmd, CancellationToken cancellationToken)
        => _service.HandleNew(cmd, cancellationToken);

    [HttpPost]
    [Route("recordPayment")]
    public Task RecordPayment([FromBody] RecordPayment cmd, CancellationToken cancellationToken)
        => _service.HandleExisting(cmd, cancellationToken);
}