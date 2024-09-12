using MediatR;
using TradeJournal.Domain.Aggregates.TradeAggregate;

namespace TradeJournal.API.Application.Commands.TradeCommands;

public class CreateStrategyCommandHandler : IRequestHandler<CreateStrategyCommand, int>
{
  private readonly ITradeRepository _tradeRepository;
  private readonly ILogger<CreateStrategyCommandHandler> _logger;

  public CreateStrategyCommandHandler(
      ITradeRepository tradeRepository,
      ILogger<CreateStrategyCommandHandler> logger)
  {
    _tradeRepository = tradeRepository;
    _logger = logger;
  }

  public async Task<int> Handle(CreateStrategyCommand command, CancellationToken cancellationToken)
  {
    var strategy = new Strategy(
        command.Name,
        command.Description
    );

    _logger.LogInformation("Creating Strategy - Strategy {@Strategy}", strategy);

    var entity = _tradeRepository.AddStrategy(strategy);
    var result = await _tradeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      _logger.LogInformation("CreateStrategyCommand succeeded");
      return entity.Id;
    }
    else
    {
      _logger.LogWarning("CreateStrategyCommand failed");
      throw new Exception("Error creating Strategy");
    }
  }
}