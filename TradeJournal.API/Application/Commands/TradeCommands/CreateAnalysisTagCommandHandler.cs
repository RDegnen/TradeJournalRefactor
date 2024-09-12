using MediatR;
using TradeJournal.Domain.Aggregates.TradeAggregate;

namespace TradeJournal.API.Application.Commands.TradeCommands;

public class CreateAnalysisTagCommandHandler : IRequestHandler<CreateAnalysisTagCommand, int>
{
  private readonly ITradeRepository _tradeRepository;
  private readonly ILogger<CreateAnalysisTagCommandHandler> _logger;

  public CreateAnalysisTagCommandHandler(
    ITradeRepository tradeRepository,
    ILogger<CreateAnalysisTagCommandHandler> logger)
  {
    _tradeRepository = tradeRepository;
    _logger = logger;
  }

  public async Task<int> Handle(CreateAnalysisTagCommand command, CancellationToken cancellationToken)
  {
    var analysisTag = new AnalysisTag(command.Name);

    _logger.LogInformation("Creating AnalysisTag - AnalysisTag {@AnalysisTag}", analysisTag);

    var entity = _tradeRepository.AddAnalysisTag(analysisTag);
    var result = await _tradeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      _logger.LogInformation("CreateAnalysisTagCommand succeeded");
      return entity.Id;
    } else
    {
      _logger.LogWarning("CreateAnalysisTagCommand failed");
      throw new Exception("Error creating AnalysisTag");
    }
  }
}
