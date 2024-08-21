using MediatR;
using TradeJournal.API.Application.DataTranserObjects;
using TradeJournal.Domain.Aggregates.TradeAggregate;

namespace TradeJournal.API.Application.Commands.TradeCommands;

public class UpdateTradeCommandHandler : IRequestHandler<UpdateTradeCommand, TradeDTO>
{
  private readonly ITradeRepository _tradeRepository;
  private readonly ILogger<UpdateTradeCommandHandler> _logger;

  public UpdateTradeCommandHandler(
    ITradeRepository tradeRepository,
    ILogger<UpdateTradeCommandHandler> logger)
  {
    _tradeRepository = tradeRepository;
    _logger = logger;
  }

  public async Task<TradeDTO> Handle(UpdateTradeCommand command, CancellationToken cancellationToken)
  {
    var trade = await _tradeRepository.GetTradeByIdAsync(command.TradeId);
    if (trade is null)
    {
      _logger.LogWarning("Trade with id {TradeId} not found", command.TradeId);
      throw new NotFoundError("Trade not found");
    }

    trade.UpdateTrade(
      command.StopLoss,
      command.TrailingStopLoss,
      command.TakeProfit,
      command.ExitTime,
      command.ExitPrice,
      command.ProfitOrLoss,
      command.Risk,
      command.Duration
    );

    _logger.LogInformation("Updating Trade - Trade {@Trade}", trade);

    _tradeRepository.Update(trade);
    var result = await _tradeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      _logger.LogInformation("UpdateTradeCommand succeeded");
      var tradeDTO = new TradeDTO(
        trade.Id,
        trade.Symbol,
        trade.Quantity,
        trade.Direction,
        trade.EntryTime,
        trade.EntryPrice,
        trade.StopLoss,
        trade.TrailingStopLoss,
        trade.TakeProfit,
        trade.ExitTime,
        trade.ExitPrice,
        trade.ProfitOrLoss,
        trade.Risk,
        trade.Duration,
        trade.Analysis,
        trade.Images.Select(i => new ImageDTO(i.Id, i.Url)).ToList()
      );
      return tradeDTO;
    } else
    {
      _logger.LogWarning("UpdateTradeCommand failed");
      throw new Exception("Error updating Trade");
    }
  }
}
