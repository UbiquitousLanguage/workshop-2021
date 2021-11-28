using Microsoft.AspNetCore.Mvc;
using static Payments.Application.PaymentCommands;

namespace Payments.Application; 

[Route("payment")]
class CommandApi : ControllerBase {
    readonly CommandService _service;
        
    public CommandApi(CommandService service) => _service = service;

    [HttpPost]
    public Task RegisterPayment([FromBody] RecordPayment cmd, CancellationToken cancellationToken)
        => _service.HandleNew(cmd, cancellationToken);
}