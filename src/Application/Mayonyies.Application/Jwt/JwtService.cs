using System.Text;
using Mayonyies.Application.Extensions;
using Mayonyies.Core;
using Mayonyies.Core.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Mayonyies.Application.Jwt;

internal sealed class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public Task<Result<string>> CreateAccessTokenAsync(
        IDictionary<string, object> claims,
        CancellationToken cancellationToken = default
    )
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));


        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            Claims = claims,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenValidityInMinutes),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var handler = new JsonWebTokenHandler
        {
            SetDefaultTimesOnTokenCreation = false
        };

        var tokenString = handler.CreateToken(descriptor);

        return Task.FromResult<Result<string>>(tokenString);
    }

    public async Task<Result<(string accessToken, Guid refreshToken)>> CreateTokensForUserAsync(
        User user,
        CancellationToken cancellationToken = default
    )
    {
        var createAccessTokenResult = await CreateAccessTokenAsync(user.GetClaims(), cancellationToken);

        if (createAccessTokenResult.IsFailure)
            return createAccessTokenResult.Error;

        var createRefreshTokenResult = await CreateRefreshTokenAsync(user, cancellationToken);

        if (createRefreshTokenResult.IsFailure)
            return createRefreshTokenResult.Error;

        return (createAccessTokenResult.Value, createRefreshTokenResult.Value.Value);
    }

    public async Task<Result<IDictionary<string, object>>> GetClaimsFromExpiredToken(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            return Errors.User.TokenIsInvalid();

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));


        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = _jwtOptions.Audience,
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = authSigningKey,
            ValidateLifetime = false,
        };

        var tokenHandler = new JsonWebTokenHandler();

        var tokenValidationResult = await tokenHandler.ValidateTokenAsync(accessToken, tokenValidationParameters);

        return tokenValidationResult.IsValid
            ? Result.Success(tokenValidationResult.Claims)
            : Errors.User.TokenIsInvalid();
    }

    public Task<Result<RefreshToken>> CreateRefreshTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var expiryTime = TimeSpan.FromDays(_jwtOptions.RefreshTokenValidityInDays);

        var addRefreshTokenResult = user.AddRefreshToken(expiryTime);

        if (addRefreshTokenResult.IsFailure)
            return Task.FromResult<Result<RefreshToken>>(addRefreshTokenResult.Error);

        return Task.FromResult<Result<RefreshToken>>(addRefreshTokenResult.Value);
    }
}