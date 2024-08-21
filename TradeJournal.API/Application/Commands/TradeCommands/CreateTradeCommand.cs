using MediatR;
using System.Runtime.Serialization;
using TradeJournal.Domain.Aggregates.TradeAggregate;

namespace TradeJournal.API.Application.Commands.TradeCommands;

[DataContract]
public class CreateTradeCommand : IRequest<int>
{
  [DataMember]
  public string Symbol { get; private set; }
  [DataMember]
  public int Quantity { get; private set; }
  [DataMember]
  public Direction Direction { get; private set; }
  [DataMember]
  public DateTime EntryTime { get; private set; }
  [DataMember]
  public double EntryPrice { get; private set; }
  [DataMember]
  public int JournalId { get; private set; }

  public CreateTradeCommand(
    string symbol,
    int quantity,
    Direction direction,
    DateTime entryTime,
    double entryPrice,
    int journalId
  )
  {
    Symbol = symbol;
    Quantity = quantity;
    Direction = direction;
    EntryTime = entryTime;
    EntryPrice = entryPrice;
    JournalId = journalId;
  }
}
