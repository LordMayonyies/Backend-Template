namespace Mayonyies.Api.Authentication;

public sealed record RefreshTokenRequest(string AccessToken, Guid RefreshToken);