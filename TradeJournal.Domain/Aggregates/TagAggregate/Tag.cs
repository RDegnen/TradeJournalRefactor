using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TagAggregate;

public class Tag : Entity, IAggregateRoot
{
  public string Name { get; private set; }
  public TagType TagType { get; private set; }

  public Tag(string name, TagType tagType)
  {
    Name = name;
    TagType = tagType;
  }
}
