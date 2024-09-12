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

  public void UpdateAnalysisList(List<Analysis> analysisList)
  {
    if (analysisList is null)
    {
      throw new ArgumentNullException(nameof(analysisList));
    }
    _analysis.Clear();
    _analysis.AddRange(analysisList);
  }
}
