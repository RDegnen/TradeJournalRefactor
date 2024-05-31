using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeJournal.Domain.Aggregates.JournalAggregate;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Infrastructure.Repositories;

public class JournalRepository : IJournalRepository
{
  private readonly TradeJournalContext _context;
  public IUnitOfWork UnitOfWork => _context;

  public JournalRepository(TradeJournalContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));
  }

  public Journal Add(Journal journal)
  {
    return _context.Journals.Add(journal).Entity;
  }
}
