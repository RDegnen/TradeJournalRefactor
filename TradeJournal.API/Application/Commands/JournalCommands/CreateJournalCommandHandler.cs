using MediatR;
using TradeJournal.Domain.Aggregates.JournalAggregate;

namespace TradeJournal.API.Application.Commands.JournalCommands;

public class CreateJournalCommandHandler : IRequestHandler<CreateJournalCommand, bool>
{
  private readonly IJournalRepository _journalRepository;
  private readonly ILogger<CreateJournalCommandHandler> _logger;

  public CreateJournalCommandHandler(IJournalRepository journalRepository, ILogger<CreateJournalCommandHandler> logger)
  {
    _journalRepository = journalRepository;
    _logger = logger;
  }

  public async Task<bool> Handle(CreateJournalCommand command, CancellationToken cancellationToken)
  {
    var journal = new Journal(command.Name, command.Description);

    _logger.LogInformation("Creating Journal - Journal {@Journal}", journal);

    _journalRepository.Add(journal);
    return await _journalRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
  }
}
