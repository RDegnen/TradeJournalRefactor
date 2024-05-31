namespace TradeJournal.Domain.Exceptions;

public class TradeJournalDomainException : Exception
{
  public TradeJournalDomainException(string message) : base(message) { }

  public TradeJournalDomainException(string message, Exception innerException) 
    : base(message, innerException) { }
}
