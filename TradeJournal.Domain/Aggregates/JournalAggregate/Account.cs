using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.JournalAggregate;

public class Account : Entity
{
  public double Balance { get; private set; }
  public double RealizedPnL { get; private set; }

  public int JournalId { get; private set; }
  public Journal Journal { get; } = null!;

  public Account(double balance)
  {
    Balance = balance;
    RealizedPnL = 0;
  }
}
