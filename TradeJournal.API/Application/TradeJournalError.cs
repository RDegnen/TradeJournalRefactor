namespace TradeJournal.API.Application;

abstract public class TradeJournalError : Exception 
{
  public TradeJournalError(string message) : base(message) { }
}

abstract public class TradeJournalHttpError : TradeJournalError
{
  public int StatusCode;

  public TradeJournalHttpError(string message, int statusCode) : base(message) 
  {
    StatusCode = statusCode;
  }
}

public class NotFoundError : TradeJournalHttpError
{
  public NotFoundError(string message) : base(message, 404) { }
}
