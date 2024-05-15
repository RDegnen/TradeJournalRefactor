using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public class AnalysisTag : Entity
{
  public string Name { get; private set; }

  private readonly List<Analysis> _analysis;
  public IReadOnlyCollection<Analysis> Analysis => _analysis.AsReadOnly();

  public AnalysisTag(string name)
  {
    Name = name;
    _analysis = new List<Analysis>();
  }
}
