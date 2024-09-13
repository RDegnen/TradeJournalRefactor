using MediatR;
using Microsoft.AspNetCore.Mvc;
using TradeJournal.API.Application.Commands.JournalCommands;
using TradeJournal.API.Application.DataTranserObjects;

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
  public async Task<ActionResult<JournalDTO>> CreateJournal(
    [FromHeader(Name = "x-requestid")] Guid requestId,
    CreateJournalRequest request)
  {
    if (requestId == Guid.Empty)
    {
      _logger.LogWarning("Invalid request - requestId is missing - {@Request}", request);
      return BadRequest("requestId is missing");
    }

    try
    {
      var command = new CreateJournalCommand(request.Name, request.Description);
      var result = await _mediator.Send(command);
      return CreatedAtAction(nameof(CreateJournal), new { id = result.Id }, result);
    }
    catch (Exception ex)
    {
      return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
  }

  [HttpPost("account")]
  public async Task<ActionResult<AccountDTO>> CreateAccount(
    [FromHeader(Name = "x-requestid")] Guid requestId,
    CreateAccountRequest request)
  {
    if (requestId == Guid.Empty)
    {
      _logger.LogWarning("Invalid request - requestId is missing - {@Request}", request);
      return BadRequest("requestId is missing");
    }

    try
    {
      var command = new CreateAccountCommand(request.JournalId, request.Balance);
      var result = await _mediator.Send(command);
      return CreatedAtAction(nameof(CreateAccount), new { id = result.Id }, result);
    }
    catch (Exception ex)
    {
      return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
  }
}

public record CreateJournalRequest(string Name, string Description);

public record CreateAccountRequest(int JournalId, double Balance);
