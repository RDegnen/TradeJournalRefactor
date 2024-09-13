using MediatR;
using TradeJournal.API.Application.DataTranserObjects;
using TradeJournal.API.Application.Queries;
using TradeJournal.Domain.Aggregates.JournalAggregate;
using TradeJournal.Infrastructure.Idempotency;

namespace TradeJournal.API.Application.Commands.JournalCommands;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDTO>
{
  private readonly IJournalRepository _journalRepository;
  private readonly ILogger<CreateAccountCommandHandler> _logger;
  private readonly IJournalQueries _journalQueries;

  public CreateAccountCommandHandler(
    IJournalRepository journalRepository,
    ILogger<CreateAccountCommandHandler> logger,
    IJournalQueries journalQueries)
  {
    _journalRepository = journalRepository;
    _logger = logger;
    _journalQueries = journalQueries;
  }

  public async Task<AccountDTO> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
  {
    var journal = await _journalQueries.GetJournalAsync(command.JournalId);
    journal.AddAccount(command.Balance);

    _logger.LogInformation("Adding account with balance - {@Balance}", command.Balance);

    var result = await _journalRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      var dto = new AccountDTO(journal.Account.Id, journal.Account.Balance, journal.Account.RealizedPnL);
      _logger.LogInformation("CreateAccountCommand succeeded");
      return dto;
    }
    else
    {
      _logger.LogWarning("CreateAccountCommand failed");
      throw new Exception("Error creating Account");
    }
  }
}
