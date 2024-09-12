using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public class Analysis : Entity
{
  public int TradeId { get; private set; }
  public Trade Trade { get; } = null!;
  public int? StrategyId { get; private set; }
  public Strategy? Strategy { get; private set; }
  public string Notes { get; private set; }

  private readonly List<AnalysisTag> _analysisTags;
  public IReadOnlyCollection<AnalysisTag> AnalysisTags => _analysisTags.AsReadOnly();

  public Analysis(int tradeId, string notes)
  {
    TradeId = tradeId;
    Notes = notes;
    _analysisTags = new List<AnalysisTag>();
  }

  public void UpdateNotes(string notes)
  {
    Notes = notes ?? throw new ArgumentNullException(nameof(notes));
  }

  public void UpdateStrategy(int strategyId, Strategy strategy)
  {
    StrategyId = strategyId;
    Strategy = strategy;
  }

  public void UpdateTags(List<AnalysisTag> tags)
  {
    if (tags is null)
    {
      throw new ArgumentException("Tags cannot be null", nameof(tags));
    }
    _analysisTags.Clear();
    _analysisTags.AddRange(tags);
  }
}
