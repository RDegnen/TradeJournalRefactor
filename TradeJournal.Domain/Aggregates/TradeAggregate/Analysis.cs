using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

  public void UpdateAnalysis(int? strategyId, string? notes)
  {
    if (strategyId.HasValue)
    {
      StrategyId = strategyId.Value;
    }
    if (!string.IsNullOrWhiteSpace(notes))
    {
      Notes = notes;
    }
  }
}
