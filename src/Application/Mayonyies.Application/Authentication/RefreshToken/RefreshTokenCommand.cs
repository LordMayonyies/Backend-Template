namespace Mayonyies.Application.Authentication.RefreshToken;

public sealed record RefreshTokenCommand(string AccessToken, Guid RefreshToken)
    : ICommand<RefreshTokenCommandResponse>;