using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommandHandler<in TComamnd> : ICommandHandler<TComamnd, Unit>
    where TComamnd : ICommand<Unit>
  {

  }

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
  {

  }
