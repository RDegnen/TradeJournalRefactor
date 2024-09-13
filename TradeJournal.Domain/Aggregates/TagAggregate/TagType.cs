using System.Text.Json.Serialization;

namespace TradeJournal.Domain.Aggregates.TagAggregate;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TagType
{
  JournalTag,
  AnalysisTag,
  ImageTag,
}
