using Mayonyies.Core.Shared;
using MediatR;

namespace Mayonyies.Application.MediatR;

internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

internal interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;