using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeJournal.Domain.Aggregates.TradeAggregate;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Infrastructure.Repositories;

public class TradeRepository : ITradeRepository
{
  private readonly TradeJournalContext _context;
  public IUnitOfWork UnitOfWork => _context;

  public TradeRepository(TradeJournalContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context)); 
  }

  public Trade Add(Trade trade)
  {
    return _context.Trades.Add(trade).Entity;
  }
}
