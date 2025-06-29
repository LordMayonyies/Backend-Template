using Mayonyies.Application.Jwt;
using Mayonyies.Core;
using Mayonyies.Core.Users;
using Microsoft.AspNetCore.Identity;

namespace Mayonyies.Application.Authentication.LogIn;

internal sealed class LogInCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    IJwtService jwtService,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<LogInCommand, LogInCommandResponse>
{
    public async Task<Result<LogInCommandResponse>> Handle(LogInCommand command, CancellationToken cancellationToken)
    {
        var user = 
            await userRepository.SingleOrDefaultAsync(user => user.Username == command.Username, cancellationToken);

        if (user is null)
            return Errors.User.UsernameOrPasswordDoesntMatch();

        if (!user.IsActive)
            return Errors.User.AccountIsNotActive();

        var passwordVerificationResult =
            passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return Errors.User.UsernameOrPasswordDoesntMatch();

        var createTokensForUserResult =
            await jwtService.CreateTokensForUserAsync(user, cancellationToken);

        if (createTokensForUserResult.IsFailure)
            return createTokensForUserResult.Error;

        var (accessToken, refreshToken) = createTokensForUserResult.Value;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new LogInCommandResponse(accessToken, refreshToken);
    }
}