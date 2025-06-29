namespace Mayonyies.Application.Authentication.RefreshToken;

public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken)
    : ICommand<RefreshTokenCommandResponse>;