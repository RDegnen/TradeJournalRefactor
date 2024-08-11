using MediatR;
using TradeJournal.API.Application.DataTranserObjects;
using TradeJournal.API.Application.Queries;
using TradeJournal.Domain.Aggregates.JournalAggregate;

namespace TradeJournal.API.Application.Commands.JournalCommands;

public class AddTagToJournalCommandHandler : IRequestHandler<AddTagToJournalCommand, JournalDTO>
{
  private readonly IJournalQueries _journalQueries;
  private readonly IJournalRepository _journalRepository;
  private readonly ILogger<AddTagToJournalCommandHandler> _logger;

  public AddTagToJournalCommandHandler(
    IJournalQueries journalQueries,
    IJournalRepository journalRepository,
    ILogger<AddTagToJournalCommandHandler> logger)
  {
    _journalQueries = journalQueries;
    _journalRepository = journalRepository;
    _logger = logger;
  }

  public async Task<JournalDTO> Handle(AddTagToJournalCommand command, CancellationToken cancellationToken)
  {
    var journal = await _journalRepository.GetJournalByIdAsync(command.JournalId);
    if (journal is null)
      throw new NotFoundError($"Journal with id {command.JournalId} not found");
    var tag = await _journalRepository.GetTagByIdAsync(command.TagId);
    if (tag is null)
      throw new NotFoundError($"Tag with id {command.TagId} not found");

    journal.AddTag(tag);
    tag.AddJournal(journal);

    var result = await _journalRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    if (result)
    {
      _logger.LogInformation("AddTagToJournalCommand succeeded");
      var journalDTO = new JournalDTO(journal.Id, journal.Name, journal.Description, journal.JournalTags.Select(t => new TagDTO(t.Id, t.Name)).ToList());
      return journalDTO;
    }
    else
    {
      _logger.LogWarning("AddTagToJournalCommand failed");
      throw new Exception("Error creating Journal Tag");
    }
  }
}
