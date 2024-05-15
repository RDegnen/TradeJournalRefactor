using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeJournal.Domain.Aggregates.JournalAggregate;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public class Trade : Entity, IAggregateRoot
{
  public string Symbol { get; private set; }
  public int Quantity { get; private set; }
  public Direction Direction { get; private set; }
  public DateTime EntryTime { get; private set; }
  public double EntryPrice { get; private set; }
  public double? StopLoss { get; private set; }
  public double? TrailingStopLoss { get; private set; }
  public double? TakeProfit {  get; private set; }
  public DateTime? ExitTime { get; private set; }
  public double? ExitPrice { get; private set; }
  public double? ProfitOrLoss { get; private set; }
  public double? Risk {  get; private set; }
  public TimeSpan? Duration { get; private set; }

  public int JournalId { get; private set; }
  public Journal Journal { get; } = null!;
  public Analysis? Analysis { get; private set; }
  private readonly List<Image> _images;
  public IReadOnlyCollection<Image> Images => _images.AsReadOnly();

  public Trade(
    int journalId,
    string symbol,
    int quantity,
    Direction direction,
    DateTime entryTime,
    double entryPrice
  )
  {
    JournalId = journalId;
    Symbol = symbol;
    Quantity = quantity;
    Direction = direction;
    EntryTime = entryTime;
    EntryPrice = entryPrice;
    _images = new List<Image>();
  }
}
