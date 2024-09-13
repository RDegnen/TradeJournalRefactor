using TradeJournal.Domain.SeedWork;
using TradeJournal.Domain.Aggregates.TagAggregate;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public class Analysis : Entity
{
  public int TradeId { get; private set; }
  public Trade Trade { get; } = null!;
  public int? StrategyId { get; private set; }
  public Strategy? Strategy { get; private set; }
  public string Notes { get; private set; }

  private readonly List<Tag> _tags;
  public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

  public Analysis(int tradeId, string notes)
  {
    TradeId = tradeId;
    Notes = notes;
    _tags = new List<Tag>();
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
}
