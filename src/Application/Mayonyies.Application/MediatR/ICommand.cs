using Mayonyies.Core.Shared;
using MediatR;

namespace Mayonyies.Application.MediatR;

public interface IBaseCommand;

public interface ICommand : IBaseCommand, IRequest<Result>;

public interface ICommand<TResponse> : IBaseCommand, IRequest<Result<TResponse>>;