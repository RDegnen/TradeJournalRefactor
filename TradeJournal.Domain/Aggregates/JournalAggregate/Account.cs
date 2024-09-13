using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.JournalAggregate;

public class Account : Entity
{
  public double Balance { get; private set; }
  public double RealizedPnL { get; private set; }

  public Account(double balance)
  {
    Balance = balance;
    RealizedPnL = 0;
  }
}
