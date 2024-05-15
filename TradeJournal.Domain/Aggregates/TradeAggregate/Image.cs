using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public class Image : Entity
{
  public string Url { get; private set; }
  
  public int TradeId { get; private set; }
  public Trade Trade { get; } = null!;
  private readonly List<ImageTag> _imageTags;
  public IReadOnlyCollection<ImageTag> ImageTags => _imageTags.AsReadOnly();

  public Image(string url, int tradeId)
  {
    Url = url;
    TradeId = tradeId;
    _imageTags = new List<ImageTag>();
  }
}
