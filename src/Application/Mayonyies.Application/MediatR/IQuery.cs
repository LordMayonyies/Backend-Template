using Mayonyies.Core.Shared;
using MediatR;

namespace Mayonyies.Application.MediatR;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;