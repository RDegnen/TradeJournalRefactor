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

  public Account AddAccount(Account account)
  {
    return _context.Accounts.Add(account).Entity;
  }

  public JournalTag AddTag(JournalTag journalTag) 
  {
    return _context.JournalTags.Add(journalTag).Entity;
  }

  public async Task<Journal?> GetJournalByIdAsync(int journalId)
  {
    return await _context.Journals.FindAsync(journalId);
  }

  public async Task<JournalTag?> GetTagByIdAsync(int tagId)
  {
    return await _context.JournalTags.FindAsync(tagId);
  }
}
