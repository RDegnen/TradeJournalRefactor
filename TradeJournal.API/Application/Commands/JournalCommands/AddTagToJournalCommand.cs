using MediatR;
using System.Runtime.Serialization;
using TradeJournal.API.Application.DataTranserObjects;

namespace TradeJournal.API.Application.Commands.JournalCommands;

[DataContract]
public class AddTagToJournalCommand : IRequest<JournalDTO>
{
  [DataMember]
  public int TagId { get; private set; }
  [DataMember]
  public int JournalId { get; private set; }

  public AddTagToJournalCommand(int tagId, int journalId)
  {
    TagId = tagId;
    JournalId = journalId;
  }
}
