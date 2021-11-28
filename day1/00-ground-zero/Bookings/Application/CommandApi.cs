using Microsoft.AspNetCore.Mvc;

namespace Bookings.Application;

[Route("/booking")]
public class CommandApi : ControllerBase {
    readonly CommandService _service;

    public CommandApi(CommandService service) => _service = service;

    [HttpPost]
    [Route("")]
    public Task DoIt([FromBody] SomeCommand cmd, CancellationToken cancellationToken)
        => _service.HandleNew(cmd, cancellationToken);
}