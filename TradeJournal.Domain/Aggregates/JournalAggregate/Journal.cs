using TradeJournal.Domain.Aggregates.TradeAggregate;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.JournalAggregate;

public class Journal : Entity, IAggregateRoot
{
  public string Name { get; private set; }
  public string Description { get; private set; }

  private readonly List<Trade> _trades;
  public IReadOnlyCollection<Trade> Trades => _trades.AsReadOnly();
  private readonly List<JournalTag> _journalTags;
  public IReadOnlyCollection<JournalTag> JournalTags => _journalTags.AsReadOnly();
  public Account? Account { get; private set; }

  public Journal(string name, string description)
  {
    Name = name;
    Description = description;
    _trades = new List<Trade>();
    _journalTags = new List<JournalTag>();
  }

  public void AddAccount(Account account)
  {
    Account = account;
  }
}
