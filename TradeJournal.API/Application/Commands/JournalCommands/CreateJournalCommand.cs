using MediatR;
using System.Runtime.Serialization;
using TradeJournal.API.Application.DataTranserObjects;

namespace TradeJournal.API.Application.Commands.JournalCommands;

[DataContract]
public class CreateJournalCommand : IRequest<JournalDTO>
{
  [DataMember]
  public string Name { get; private set; }
  [DataMember]
  public string Description { get; private set; }
  
  public CreateJournalCommand(string name, string description)
  {
    Name = name;
    Description = description;
  }
}
