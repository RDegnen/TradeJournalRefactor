using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public class Strategy : Entity
{
  public string Name { get; private set; }
  public string Description { get; private set; }

  private readonly List<Analysis> _analysis;
  public IReadOnlyCollection<Analysis> Analysis => _analysis.AsReadOnly();

  public Strategy(string name, string description) 
  {
    Name = name;
    Description = description;
    _analysis = new List<Analysis>();
  }
}
