using MediatR;
using System.Runtime.Serialization;

namespace TradeJournal.API.Application.Commands.TradeCommands;

[DataContract]
public class CreateAnalysisTagCommand : IRequest<int>
{
  [DataMember]
  public string Name { get; private set; }

  public CreateAnalysisTagCommand(string name)
  {
    Name = name;
  }
}

