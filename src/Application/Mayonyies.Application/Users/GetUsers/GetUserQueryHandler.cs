namespace Mayonyies.Application.Users.GetUsers;

internal sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResponse>
{
    public Task<Result<GetUserQueryResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}