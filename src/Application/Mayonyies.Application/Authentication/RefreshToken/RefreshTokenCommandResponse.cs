namespace Mayonyies.Application.Authentication.RefreshToken;

public sealed record RefreshTokenCommandResponse(string AccessToken, Guid RefreshToken);