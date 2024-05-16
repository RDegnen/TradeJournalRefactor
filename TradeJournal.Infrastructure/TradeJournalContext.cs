using Microsoft.EntityFrameworkCore;
using TradeJournal.Domain.SeedWork;
using TradeJournal.Domain.Aggregates.JournalAggregate;
using TradeJournal.Domain.Aggregates.TradeAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace TradeJournal.Infrastructure;

public class TradeJournalContext : DbContext, IUnitOfWork
{
  // Journal Aggregate
  public DbSet<Journal> Journals { get; set; }
  public DbSet<JournalTag> Tags { get; set; }
  public DbSet<Account> Accounts { get; set; }
  // Trade Aggregate
  public DbSet<Trade> Trades { get; set; }
  public DbSet<Analysis> Analysis { get; set; }
  public DbSet<AnalysisTag> AnalysisTags { get; set; }
  public DbSet<Image> Images { get; set; }
  public DbSet<ImageTag> ImageTags { get; set; }
  public DbSet<Strategy> Strategies { get; set; }

  private readonly IMediator _mediator;
  private IDbContextTransaction _currentTransaction;

  public TradeJournalContext(DbContextOptions<TradeJournalContext> options) : base(options) { }

  public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

  public bool HasActiveTransaction => _currentTransaction != null;

  public TradeJournalContext(DbContextOptions<TradeJournalContext> options, IMediator mediator) : base(options)
  {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
  {
    await _mediator.DispatchDomainEventsAsync(this);
    _ = await base.SaveChangesAsync(cancellationToken);
    return true;
  }

  public async Task<IDbContextTransaction> BeginTransactionAsync()
  {
    if (_currentTransaction != null) return null;
    _currentTransaction = await Database.BeginTransactionAsync(default);
    return _currentTransaction;
  }

  public async Task CommitTransactionAsync(IDbContextTransaction transaction)
  {
    if (transaction == null) throw new ArgumentNullException(nameof(transaction));
    if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

    try
    {
      await SaveChangesAsync();
      await transaction.CommitAsync();
    }
    catch
    {
      RollbackTransaction();
      throw;
    }
    finally
    {
      if (_currentTransaction != null)
      {
        _currentTransaction.Dispose();
        _currentTransaction = null;
      }
    }
  }

  public void RollbackTransaction()
  {
    try
    {
      _currentTransaction?.Rollback();
    }
    finally
    {
      if (_currentTransaction != null)
      {
        _currentTransaction.Dispose();
        _currentTransaction = null;
      }
    }
  }
}
