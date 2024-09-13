using TradeJournal.Domain.Aggregates.TradeAggregate;
using TradeJournal.Domain.Aggregates.TagAggregate;
using TradeJournal.Domain.SeedWork;

namespace TradeJournal.Domain.Aggregates.JournalAggregate;

public class Journal : Entity, IAggregateRoot
{
  public string Name { get; private set; }
  public string Description { get; private set; }

  private readonly List<Trade> _trades;
  public IReadOnlyCollection<Trade> Trades => _trades.AsReadOnly();
  public Account? Account { get; private set; }
  private readonly List<Tag> _tags;
  public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

  public Journal(string name, string description)
  {
    Name = name;
    Description = description;
    _trades = new List<Trade>();
    _tags = new List<Tag>();
  }

  public void AddAccount(Account account)
  {
    Account = account;
  }
}
