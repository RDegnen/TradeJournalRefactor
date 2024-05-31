using MediatR;
using System.Runtime.Serialization;

namespace TradeJournal.API.Application.Commands.JournalCommands;

[DataContract]
public class CreateJournalCommand : IRequest<bool>
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
