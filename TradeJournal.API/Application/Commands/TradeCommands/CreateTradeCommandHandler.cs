using MediatR;
using TradeJournal.Domain.Aggregates.TradeAggregate;

namespace TradeJournal.API.Application.Commands.TradeCommands;

public class CreateTradeCommandHandler : IRequestHandler<CreateTradeCommand, int>
{
  private readonly ITradeRepository _tradeRepository;
  private readonly ILogger<CreateTradeCommandHandler> _logger;

  public CreateTradeCommandHandler(
    ITradeRepository tradeRepository,
    ILogger<CreateTradeCommandHandler> logger)
  {
    _tradeRepository = tradeRepository;
    _logger = logger;
  }

  public async Task<int> Handle(CreateTradeCommand command, CancellationToken cancellationToken)
  {
    var trade = new Trade(
      command.JournalId,
      command.Symbol,
      command.Quantity,
      command.Direction,
      command.EntryTime,
      command.EntryPrice
    );

    _logger.LogInformation("Creating Trade - Trade {@Trade}", trade);

    var entity = _tradeRepository.Add(trade);
    var result = await _tradeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      _logger.LogInformation("CreateTradeCommand succeeded");
      return entity.Id;
    } else
    {
      _logger.LogWarning("CreateTradeCommand failed");
      throw new Exception("Error creating Trade");
    }
  }
}
