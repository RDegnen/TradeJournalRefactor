using System.Text.Json.Serialization;

namespace TradeJournal.Domain.Aggregates.TradeAggregate;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Direction
{
  Buy = 1, Sell = 2,
}
