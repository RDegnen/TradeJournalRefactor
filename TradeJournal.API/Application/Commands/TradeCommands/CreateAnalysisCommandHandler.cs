using MediatR;
using TradeJournal.Domain.Aggregates.TradeAggregate;

namespace TradeJournal.API.Application.Commands.TradeCommands;

public class CreateAnalysisCommandHandler : IRequestHandler<CreateAnalysisCommand, int>
{
  private readonly ITradeRepository _tradeRepository;
  private readonly ILogger<CreateAnalysisCommandHandler> _logger;

  public CreateAnalysisCommandHandler(
    ITradeRepository tradeRepository,
    ILogger<CreateAnalysisCommandHandler> logger)
  {
    _tradeRepository = tradeRepository;
    _logger = logger;
  }

  public async Task<int> Handle(CreateAnalysisCommand command, CancellationToken cancellationToken)
  {
    var Analysis = new Analysis(
      command.TradeId,
      command.Notes
    );

    if (command.StrategyId.HasValue)
    {
      var strategy = await _tradeRepository.GetStrategyByIdAsync(command.StrategyId.Value);
      if (strategy is null)
      {
        _logger.LogWarning("CreateAnalysisCommand failed - Strategy not found");
        throw new NotFoundError($"Strategy with id {command.StrategyId} not found");
      }
      Analysis.UpdateStrategy(command.StrategyId.Value, strategy);
    }

    _logger.LogInformation("Creating Analysis - Analysis {@Analysis}", Analysis);

    var entity = _tradeRepository.AddAnalysis(Analysis);
    var result = await _tradeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      _logger.LogInformation("CreateAnalysisCommand succeeded");
      return entity.Id;
    } else
    {
      _logger.LogWarning("CreateAnalysisCommand failed");
      throw new Exception("Error creating Analysis");
    }
  }
}
