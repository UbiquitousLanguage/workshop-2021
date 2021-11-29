using Microsoft.AspNetCore.Mvc;
using static Bookings.Application.Bookings.BookingCommands;

namespace Bookings.Application.Bookings;

[Route("/booking")]
public class CommandApi : ControllerBase {
    readonly BookingsCommandService _service;

    public CommandApi(BookingsCommandService service) => _service = service;

    [HttpPost]
    [Route("")]
    public Task Book([FromBody] Book cmd, CancellationToken cancellationToken)
        => _service.HandleNew(cmd, cancellationToken);

    [HttpPost]
    [Route("pay")]
    public Task ConfirmPayment([FromBody] RecordPayment cmd, CancellationToken cancellationToken)
        => _service.HandleExisting(cmd, cancellationToken);

    [HttpPost]
    [Route("discount")]
    public Task ApplyDiscount([FromBody] ApplyDiscount cmd, CancellationToken cancellationToken)
        => _service.HandleExisting(cmd, cancellationToken);

    [HttpPost]
    [Route("cancel")]
    public Task Cancel([FromBody] CancelBooking cmd, CancellationToken cancellationToken)
        => _service.HandleExisting(cmd, cancellationToken);
}