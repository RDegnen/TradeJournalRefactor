using MediatR;
using System.Runtime.Serialization;

namespace TradeJournal.API.Application.Commands.TradeCommands;

[DataContract]
public class CreateAnalysisCommand : IRequest<int>
{
  [DataMember]
  public int TradeId { get; private set; }
  [DataMember]
  public int? StrategyId { get; private set; }
  [DataMember]
  public string Notes { get; private set; }

  public CreateAnalysisCommand(
    int tradeId,
    int? strategyId,
    string notes
  )
  {
    TradeId = tradeId;
    StrategyId = strategyId;
    Notes = notes;
  }
}
