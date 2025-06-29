namespace Mayonyies.Application.Authentication.LogIn;

public sealed record LogInCommandResponse(string AccessToken, Guid RefreshToken);