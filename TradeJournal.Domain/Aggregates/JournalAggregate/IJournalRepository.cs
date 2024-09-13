using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.JournalAggregate;

public interface IJournalRepository : IRepository<Journal>
{
  Journal Add(Journal journal);

  Task<Journal?> GetJournalByIdAsync(int jounrnalId);
}
