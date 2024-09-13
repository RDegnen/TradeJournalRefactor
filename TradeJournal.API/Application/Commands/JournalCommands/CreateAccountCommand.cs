using MediatR;
using TradeJournal.API.Application.DataTranserObjects;
using System.Runtime.Serialization;

namespace TradeJournal.API.Application.Commands.JournalCommands;

[DataContract]
public class CreateAccountCommand : IRequest<AccountDTO>
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
