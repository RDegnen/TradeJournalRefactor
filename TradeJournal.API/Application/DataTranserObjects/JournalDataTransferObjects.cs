namespace TradeJournal.API.Application.DataTranserObjects;

public record JournalDTO(int Id, string Name, string Description, List<TagDTO> Tags);

public record TagDTO(int Id, string Name);

public record AccountDTO(int Id, double Balance, double RealizedPnL);