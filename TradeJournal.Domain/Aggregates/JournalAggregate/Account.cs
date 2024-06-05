using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.JournalAggregate;

public class Account : Entity
{
  public double Balance { get; private set; }
  public double RealizedPnL { get; private set; }

  public int JournalId { get; private set; }
  public Journal Journal { get; } = null!;

  public Account(int journalId, double balance)
  {
    JournalId = journalId;
    Balance = balance;
    RealizedPnL = 0;
  }
}
