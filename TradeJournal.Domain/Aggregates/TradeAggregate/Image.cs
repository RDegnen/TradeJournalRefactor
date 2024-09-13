using TradeJournal.Domain.Aggregates.TagAggregate;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public class Image : Entity
{
  public string Url { get; private set; }
  
  public int TradeId { get; private set; }
  public Trade Trade { get; } = null!;

  private readonly List<Tag> _tags;
  public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

  public Image(string url, int tradeId)
  {
    Url = url;
    TradeId = tradeId;
    _tags = new List<Tag>();
  }
}
