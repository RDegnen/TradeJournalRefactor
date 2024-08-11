using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.JournalAggregate;

public interface IJournalRepository : IRepository<Journal>
{
  Journal Add(Journal journal);

  Account AddAccount(Account account);

  JournalTag AddTag(JournalTag journalTag);

  Task<Journal?> GetJournalByIdAsync(int jounrnalId);

  Task<JournalTag?> GetTagByIdAsync(int tagId);
}
