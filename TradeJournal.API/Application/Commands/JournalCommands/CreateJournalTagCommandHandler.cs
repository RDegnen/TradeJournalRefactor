using MediatR;
using TradeJournal.Domain.Aggregates.JournalAggregate;

namespace TradeJournal.API.Application.Commands.JournalCommands;

public class CreateJournalTagCommandHandler : IRequestHandler<CreateJournalTagCommand, int>
{
  private readonly IJournalRepository _journalRepository;
  private readonly ILogger<CreateJournalTagCommandHandler> _logger;

  public CreateJournalTagCommandHandler(IJournalRepository journalRepository, ILogger<CreateJournalTagCommandHandler> logger)
  {
    _journalRepository = journalRepository;
    _logger = logger;
  }

  public async Task<int> Handle(CreateJournalTagCommand command, CancellationToken cancellationToken)
  {
    var tag = new JournalTag(command.Name);

    _logger.LogInformation("Adding journal tag - {@Tag}", tag);

    var entity = _journalRepository.AddTag(tag);
    var result = await _journalRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      _logger.LogInformation("CreateJournalTagCommand succeeded");
      return entity.Id;
    }
    else
    {
      _logger.LogWarning("CreateJournalTagCommand failed");
      throw new Exception("Error creating Journal Tag");
    }
  }
}
