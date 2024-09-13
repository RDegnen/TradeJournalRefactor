using TradeJournal.Domain.Aggregates.TradeAggregate;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Infrastructure.Repositories;

public class TradeRepository : ITradeRepository
{
  private readonly TradeJournalContext _context;
  public IUnitOfWork UnitOfWork => _context;

  public TradeRepository(TradeJournalContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));
  }

  public Trade Add(Trade trade)
  {
    return _context.Trades.Add(trade).Entity;
  }

  public Trade Update(Trade trade)
  {
    return _context.Trades.Update(trade).Entity;
  }

  public async Task<Trade?> GetTradeByIdAsync(int tradeId)
  {
    return await _context.Trades.FindAsync(tradeId);
  }

  public Analysis AddAnalysis(Analysis analysis)
  {
    return _context.Analysis.Add(analysis).Entity;
  }

  public Strategy AddStrategy(Strategy strategy)
  {
    return _context.Strategies.Add(strategy).Entity;
  }

  public async Task<Strategy?> GetStrategyByIdAsync(int strategyId)
  {
    return await _context.Strategies.FindAsync(strategyId);
  }
}
