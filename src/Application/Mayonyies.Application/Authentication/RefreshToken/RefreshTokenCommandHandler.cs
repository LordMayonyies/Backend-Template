using Mayonyies.Application.Jwt;
using Mayonyies.Core;
using Mayonyies.Core.Users;

namespace Mayonyies.Application.Authentication.RefreshToken;

internal sealed class RefreshTokenCommandHandler(
    IJwtService jwtService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
{
    public async Task<Result<RefreshTokenCommandResponse>> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken
    )
    {
        var (_, isFailure, claims, error) = await jwtService.GetClaimsFromExpiredToken(command.AccessToken);

        if (isFailure)
            return error;

        if (!claims.TryGetValue(JwtClaimTypes.UserId, out var userIdValue) ||
            !int.TryParse(userIdValue.ToString(), out var userId))
            return Errors.User.TokenIsInvalid();

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return Errors.User.TokenIsInvalid();

        (_, isFailure, var (accessToken, refreshToken), error) = await jwtService.CreateTokensForUserAsync(user, cancellationToken);

        if (isFailure)
            return error;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RefreshTokenCommandResponse(accessToken, refreshToken);
    }
}