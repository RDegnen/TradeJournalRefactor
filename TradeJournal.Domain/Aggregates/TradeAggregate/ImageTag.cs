using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public class ImageTag : Entity
{
  public string Name { get; private set; }

  private readonly List<Image> _images;
  public IReadOnlyCollection<Image> Images => _images.AsReadOnly();

  public ImageTag(string name)
  {
    Name = name;
    _images = new List<Image>();
  }
}
