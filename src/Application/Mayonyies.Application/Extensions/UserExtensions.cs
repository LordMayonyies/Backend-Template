using Mayonyies.Application.Jwt;
using Mayonyies.Core.Users;

namespace Mayonyies.Application.Extensions;

internal static class UserExtensions
{
    internal static IDictionary<string, object> GetClaims(this User user)
    {
        return new Dictionary<string, object>
        {
            { JwtClaimTypes.UserId, user.Id },
            { JwtClaimTypes.Username, user.Username }
        };
    }
}