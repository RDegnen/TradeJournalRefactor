﻿using MediatR;
using TradeJournal.Domain.Aggregates.JournalAggregate;
using TradeJournal.Infrastructure.Idempotency;

namespace TradeJournal.API.Application.Commands.JournalCommands;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
{
  private readonly IJournalRepository _journalRepository;
  private readonly ILogger<CreateAccountCommandHandler> _logger;

  public CreateAccountCommandHandler(
    IJournalRepository journalRepository,
    ILogger<CreateAccountCommandHandler> logger)
  {
    _journalRepository = journalRepository;
    _logger = logger;
  }

  public async Task<int> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
  {
    var account = new Account(command.JournalId, command.Balance);

    _logger.LogInformation("Adding Account - {@Account}", account);

    var entity = _journalRepository.AddAccount(account);
    var result = await _journalRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      _logger.LogInformation("CreateAccountCommand succeeded");
      return entity.Id;
    }
    else
    {
      _logger.LogWarning("CreateAccountCommand failed");
      throw new Exception("Error creating Account");
    }
  }
}

public class CreateAccountIdentifiedCommandHandler
  : IdentifiedCommandHandler<CreateAccountCommand, int>
{
  public CreateAccountIdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<CreateAccountCommand, int>> logger)
      : base(mediator, requestManager, logger) { }

  protected override int CreateResultForDuplicateRequest()
  {
    return 0;
  }
}