using MediatR;
using Microsoft.AspNetCore.Mvc;
using TradeJournal.API.Application;
using TradeJournal.API.Application.Commands.TradeCommands;
using TradeJournal.API.Application.DataTranserObjects;
using TradeJournal.Domain.Aggregates.TradeAggregate;

namespace TradeJournal.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TradesController : ControllerBase
{
  private readonly IMediator _mediator;
  private readonly ILogger<TradesController> _logger;

  public TradesController(
    IMediator mediator,
    ILogger<TradesController> logger)
  {
    _mediator = mediator;
    _logger = logger;
  }

  [HttpPost]
  public async Task<ActionResult<int>> createTrade(CreateTradeRequest request)
  {
    try
    {
      var command = new CreateTradeCommand(
        request.Symbol,
        request.Quantity,
        request.Direction,
        request.EntryTime,
        request.EntryPrice,
        request.JournalId
      );
      var tradeId = await _mediator.Send(command);
      return Ok(tradeId);
    }
    catch (TradeJournalHttpError ex)
    {
      return Problem(detail: ex.Message, statusCode: ex.StatusCode);
    }
    catch (Exception ex)
    {
      return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
  }

  [HttpPut("{tradeId}")]
  public async Task<ActionResult<TradeDTO>> updateTrade(int tradeId, UpdateTradeRequest request)
  {
    try
    {
      var command = new UpdateTradeCommand(
        tradeId,
        request.StopLoss,
        request.TrailingStopLoss,
        request.TakeProfit,
        request.ExitTime,
        request.ExitPrice,
        request.ProfitOrLoss,
        request.Risk,
        request.Duration
      );
      var trade = await _mediator.Send(command);
      return Ok(trade);
    }
    catch (TradeJournalHttpError ex)
    {
      return Problem(detail: ex.Message, statusCode: ex.StatusCode);
    }
    catch (Exception ex)
    {
      return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
  }

  [HttpPost("analysis")]
  public async Task<ActionResult<int>> createAnalysis(CreateAnalysisRequest request)
  {
    try
    {
      var command = new CreateAnalysisCommand(
        request.TradeId,
        request.StrategyId,
        request.Notes
      );
      var analysisId = await _mediator.Send(command);
      return Ok(analysisId);
    }
    catch (TradeJournalHttpError ex)
    {
      return Problem(detail: ex.Message, statusCode: ex.StatusCode);
    }
    catch (Exception ex)
    {
      return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
  }

  [HttpPost("analysis/tags")]
  public async Task<ActionResult<int>> createAnalysisTag(CreateAnalysisTagRequest request)
  {
    try
    {
      var command = new CreateAnalysisTagCommand(request.Name);
      var analysisTagId = await _mediator.Send(command);
      return Ok(analysisTagId);
    }
    catch (TradeJournalHttpError ex)
    {
      return Problem(detail: ex.Message, statusCode: ex.StatusCode);
    }
    catch (Exception ex)
    {
      return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
  }

  [HttpPost("strategies")]
  public async Task<ActionResult<int>> createStrategy(CreateStrategyRequest request)
  {
    try
    {
      var command = new CreateStrategyCommand(request.Name, request.Description);
      var strategyId = await _mediator.Send(command);
      return Ok(strategyId);
    }
    catch (TradeJournalHttpError ex)
    {
      return Problem(detail: ex.Message, statusCode: ex.StatusCode);
    }
    catch (Exception ex)
    {
      return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
  }
}

public record CreateTradeRequest(
  string Symbol,
  int Quantity,
  Direction Direction,
  DateTime EntryTime,
  double EntryPrice,
  int JournalId
);

public record UpdateTradeRequest(
  double? StopLoss,
  double? TrailingStopLoss,
  double? TakeProfit,
  DateTime? ExitTime,
  double? ExitPrice,
  double? ProfitOrLoss,
  double? Risk,
  TimeSpan? Duration
);

public record CreateAnalysisRequest(
  int TradeId,
  int? StrategyId,
  string Notes
);

public record CreateAnalysisTagRequest(
  string Name
);

public record CreateStrategyRequest(
  string Name,
  string Description
);