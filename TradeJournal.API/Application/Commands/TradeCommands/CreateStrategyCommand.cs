using MediatR;

namespace TradeJournal.API.Application.Commands.TradeCommands;

public class CreateStrategyCommand : IRequest<int>
{
  public string Name { get; private set; }
  public string Description { get; private set; }

  public CreateStrategyCommand(string name, string description)
  {
    Name = name;
    Description = description;
  }
}

