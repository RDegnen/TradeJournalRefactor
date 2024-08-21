using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public interface ITradeRepository : IRepository<Trade>
{
  Trade Add(Trade trade);

  Trade Update(Trade trade);

  Task<Trade?> GetTradeByIdAsync(int tradeId);

  Analysis AddAnalysis(Analysis analysis);
}
