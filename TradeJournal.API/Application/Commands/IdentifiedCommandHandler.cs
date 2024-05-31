using MediatR;
using TradeJournal.Infrastructure.Idempotency;
using TradeJournal.API.Extensions;

namespace TradeJournal.API.Application.Commands;

public abstract class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R>
  where T : IRequest<R>
{
  private readonly IMediator _mediator;
  private readonly IRequestManager _requestManager;
  private readonly ILogger<IdentifiedCommandHandler<T, R>> _logger;

  public IdentifiedCommandHandler(
    IMediator mediator,
    IRequestManager requestManager,
    ILogger<IdentifiedCommandHandler<T, R>> logger)
  {
    _mediator = mediator;
    _requestManager = requestManager;
    _logger = logger;
  }

  protected abstract R CreateResultForDuplicateRequest();

  public async Task<R> Handle(IdentifiedCommand<T, R> message, CancellationToken cancellationToken)
  {
    var alreadyExists = await _requestManager.ExistsAsync(message.Id);
    if (alreadyExists)
    {
      return CreateResultForDuplicateRequest();
    }
    else
    {
      await _requestManager.CreateRequestForCommandAsync<T>(message.Id);
      try
      {
        var command = message.Command;

        _logger.LogInformation(
          "Sending command: {CommandName}: {@Command}", 
          command.GetGenericTypeName(),
          command);

        var result = await _mediator.Send(message.Command);

        _logger.LogInformation(
          "Command result: {@Result} - {CommandName}: {@Command}",
          result,
          command.GetGenericTypeName(),
          command);

        return result;
      }
      catch
      {
        return default;
      }
    }
  }
}
