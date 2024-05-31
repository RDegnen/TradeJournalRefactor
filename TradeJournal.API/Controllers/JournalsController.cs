using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TradeJournal.API.Application.Commands;
using TradeJournal.API.Application.Commands.JournalCommands;

namespace TradeJournal.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JournalsController : ControllerBase
{
  private readonly IMediator _mediator;
  private readonly ILogger<JournalsController> _logger;

  public JournalsController(
    IMediator mediator,
    ILogger<JournalsController> logger) 
  {
    _mediator = mediator;
    _logger = logger;
  }

  [HttpPost]
  public async Task<Results<Ok, BadRequest<string>>> CreateJournal(
    [FromHeader(Name = "x-requestid")] Guid requestId,
    CreateJournalRequest request)
  {
    if (requestId == Guid.Empty)
    {
      _logger.LogWarning("Invalid request - requestId is missing - {@Request}", request);
      return TypedResults.BadRequest("requestId is missing");
    }

    var command = new CreateJournalCommand(request.Name, request.Description);
    var identifiedCommand = new IdentifiedCommand<CreateJournalCommand, bool>(command, requestId);
    var result = await _mediator.Send(identifiedCommand);

    if (result)
    {
      _logger.LogInformation("CreateJournalCommand succeeded");
    }
    else
    {
      _logger.LogWarning("CreateJournalCommand failed");
    }

    return TypedResults.Ok();
  }
}

public record CreateJournalRequest(string Name, string Description);