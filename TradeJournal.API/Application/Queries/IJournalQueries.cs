using TradeJournal.Domain.Aggregates.JournalAggregate;

namespace TradeJournal.API.Application.Queries;

public interface IJournalQueries
{
  Task<Journal> GetJournalAsync(int id);
}
