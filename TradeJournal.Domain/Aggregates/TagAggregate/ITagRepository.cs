using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TagAggregate;

public interface ITagRepository : IRepository<Tag>
{
  Tag AddTag(Tag tag);
}
