using TradeJournal.Domain.SeedWork;
using TradeJournal.Domain.Aggregates.JournalAggregate;
using TradeJournal.Domain.Aggregates.TradeAggregate;

namespace TradeJournal.Domain.Aggregates.TagAggregate;

public class Tag : Entity, IAggregateRoot
{
  public string Name { get; private set; }
  public TagType TagType { get; private set; }
  private readonly List<Journal>? _journals;
  public IReadOnlyCollection<Journal>? Journals => _journals.AsReadOnly();
  private readonly List<Analysis>? _anlysis;
  public IReadOnlyCollection<Analysis>? Analysis => _anlysis.AsReadOnly();
  private readonly List<Image>? _images;
  public IReadOnlyCollection<Image>? Images => _images.AsReadOnly();

  public Tag(string name, TagType tagType)
  {
    Name = name;
    TagType = tagType;
  }
}
