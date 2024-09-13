using MediatR;
using TradeJournal.API.Application.DataTranserObjects;
using TradeJournal.Domain.Aggregates.JournalAggregate;

namespace TradeJournal.API.Application.Commands.JournalCommands;

public class CreateJournalCommandHandler : IRequestHandler<CreateJournalCommand, JournalDTO>
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

  public async Task<JournalDTO> Handle(CreateJournalCommand command, CancellationToken cancellationToken)
  {
    var journal = new Journal(command.Name, command.Description);

    _logger.LogInformation("Creating Journal - Journal {@Journal}", journal);

    var entity = _journalRepository.Add(journal);
    var result = await _journalRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      var dto = new JournalDTO(Name: entity.Name, Description: entity.Description, Id: entity.Id, Tags: entity.Tags.Select(tag => new TagDTO(tag.Id, tag.Name)).ToList());
      _logger.LogInformation("CreateJournalCommand succeeded");
      return dto;
    }
    else
    {
      _logger.LogWarning("CreateJournalCommand failed");
      throw new Exception("Error creating journal");
    }
  }
}
