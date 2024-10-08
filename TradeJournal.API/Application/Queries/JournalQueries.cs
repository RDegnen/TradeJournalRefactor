﻿using TradeJournal.Domain.Aggregates.JournalAggregate;
using TradeJournal.Infrastructure;

namespace TradeJournal.API.Application.Queries;

public class JournalQueries : IJournalQueries
{
  private readonly TradeJournalContext _context;

  public JournalQueries(TradeJournalContext context)
  {
    _context = context;
  }

  public async Task<Journal> GetJournalAsync(int id) 
  {
    var journal = await _context.Journals.FindAsync(id);

    if (journal is null) throw new KeyNotFoundException();

    return journal;
  }
}
