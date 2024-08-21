using TradeJournal.Domain.Aggregates.TradeAggregate;

namespace TradeJournal.API.Application.DataTranserObjects;

public record TradeDTO(
  int Id,
  string Symbol,
  int Quantity,
  Direction Direction,
  DateTime EntryTime,
  double EntryPrice,
  double? StopLoss,
  double? TrailingStopLoss,
  double? TakeProfit,
  DateTime? ExitTime,
  double? ExitPrice,
  double? ProfitOrLoss,
  double? Risk,
  TimeSpan? Duration,
  Analysis? Analysis,
  List<ImageDTO> Images);

public record ImageDTO(int Id, string Url);
