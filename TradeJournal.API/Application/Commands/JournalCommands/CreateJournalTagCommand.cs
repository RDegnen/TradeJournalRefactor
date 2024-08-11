using MediatR;
using System.Runtime.Serialization;

namespace TradeJournal.API.Application.Commands.JournalCommands;

[DataContract]
public class CreateJournalTagCommand : IRequest<int>
{
  [DataMember]
  public string Name { get; private set; }

  public CreateJournalTagCommand(string name)
  {
    Name = name;
  }
}

