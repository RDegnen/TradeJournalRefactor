using MediatR;
using TradeJournal.Domain.Aggregates.JournalAggregate;
using TradeJournal.Infrastructure.Idempotency;

namespace TradeJournal.API.Application.Commands.JournalCommands;

public class CreateJournalCommandHandler : IRequestHandler<CreateJournalCommand, int>
{
  private readonly IJournalRepository _journalRepository;
  private readonly ILogger<CreateJournalCommandHandler> _logger;

  public CreateJournalCommandHandler(
    IJournalRepository journalRepository, 
    ILogger<CreateJournalCommandHandler> logger)
  {
    _journalRepository = journalRepository;
    _logger = logger;
  }

  public async Task<int> Handle(CreateJournalCommand command, CancellationToken cancellationToken)
  {
    var journal = new Journal(command.Name, command.Description);

    _logger.LogInformation("Creating Journal - Journal {@Journal}", journal);

    var entity = _journalRepository.Add(journal);
    var result = await _journalRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      _logger.LogInformation("CreateJournalCommand succeeded");
      return entity.Id;
    }
    else
    {
      _logger.LogWarning("CreateJournalCommand failed");
      throw new Exception("Error creating journal");
    }
  }
}

public class CreateJournalIdentifiedCommandHandler : IdentifiedCommandHandler<CreateJournalCommand, int>
{
  public CreateJournalIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateJournalCommand, int>> logger)
      : base(mediator, requestManager, logger) { }

  protected override int CreateResultForDuplicateRequest()
  {
    return 0;
  }
}