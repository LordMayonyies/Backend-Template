using Mayonyies.Core.Shared;
using MediatR;

namespace Mayonyies.Application.MediatR;

public interface IQueryHandler<TQuery, TQueryResponse> : IRequestHandler<TQuery, Result<TQueryResponse>>
    where TQuery : IQuery<TQueryResponse>;