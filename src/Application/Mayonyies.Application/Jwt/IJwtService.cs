using System.Security.Claims;
using Mayonyies.Core.Users;

namespace Mayonyies.Application.Jwt;

public interface IJwtService
{
    Task<Result<string>> CreateAccessTokenAsync(
        IDictionary<string, object> claims,
        CancellationToken cancellationToken = default);

    Task<Result<(string accessToken, Guid refreshToken)>> CreateTokensForUserAsync(
        User user,
        CancellationToken cancellationToken = default);

    Task<Result<IDictionary<string, object>>> GetClaimsFromExpiredToken(string accessToken);
    Task<Result<RefreshToken>> CreateRefreshTokenAsync(User user, CancellationToken cancellationToken = default);
}