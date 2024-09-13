using TradeJournal.Domain.Aggregates.TagAggregate;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
  private readonly TradeJournalContext _context;
  public IUnitOfWork UnitOfWork => _context;

  public TagRepository(TradeJournalContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));
  }

  public Tag AddTag(Tag tag)
  {
    return _context.Tags.Add(tag).Entity;
  }
}
