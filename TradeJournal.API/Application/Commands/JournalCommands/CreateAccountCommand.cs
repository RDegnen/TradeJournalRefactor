using MediatR;
using System.Runtime.Serialization;

namespace TradeJournal.API.Application.Commands.JournalCommands;

[DataContract]
public class CreateAccountCommand : IRequest<int>
{
  [DataMember]
  public int JournalId { get; private set; }
  [DataMember]
  public double Balance { get; private set; }

  public CreateAccountCommand(int journalId, double balance)
  {
    JournalId = journalId;
    Balance = balance;
  }
}
