using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

public interface ITradeRepository : IRepository<Trade>
{
  Trade Add(Trade trade);
}
