using MediatR;
using System.Runtime.Serialization;
using TradeJournal.API.Application.DataTranserObjects;

namespace TradeJournal.API.Application.Commands.TradeCommands;

[DataContract]
public class UpdateTradeCommand : IRequest<TradeDTO>
{
  [DataMember]
  public int TradeId { get; private set; }
  [DataMember]
  public double? StopLoss { get; private set; }
  [DataMember]
  public double? TrailingStopLoss { get; private set; }
  [DataMember]
  public double? TakeProfit { get; private set; }
  [DataMember]
  public DateTime? ExitTime { get; private set; }
  [DataMember]
  public double? ExitPrice { get; private set; }
  [DataMember]
  public double? ProfitOrLoss { get; private set; }
  [DataMember]
  public double? Risk { get; private set; }
  [DataMember]
  public TimeSpan? Duration { get; private set; }

  public UpdateTradeCommand(
    int tradeId,
    double? stopLoss,
    double? trailingStopLoss,
    double? takeProfit,
    DateTime? exitTime,
    double? exitPrice,
    double? profitOrLoss,
    double? risk,
    TimeSpan? duration
  )
  {
    TradeId = tradeId;
    StopLoss = stopLoss;
    TrailingStopLoss = trailingStopLoss;
    TakeProfit = takeProfit;
    ExitTime = exitTime;
    ExitPrice = exitPrice;
    ProfitOrLoss = profitOrLoss;
    Risk = risk;
    Duration = duration;
  }
}
