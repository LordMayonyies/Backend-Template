using Mayonyies.Core.Shared;

namespace Mayonyies.Application.Messaging;

public interface IQueryHandler<in TQuery, TQueryResponse>
    where TQuery : IQuery<TQueryResponse>
{
    Task<Result<TQueryResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}