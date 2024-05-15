using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.JournalAggregate;

public class JournalTag : Entity
{
  public string Name { get; private set; }

  private readonly List<Journal> _journals;
  public IReadOnlyCollection<Journal> Journals => _journals.AsReadOnly();

  public JournalTag(string name)
  {
    Name = name;
    _journals = new List<Journal>();
  }
}
