namespace Mayonyies.Application.Jwt;

public sealed record JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string SecretKey { get; init; } = null!;
    public int AccessTokenValidityInMinutes { get; init; } = 5;
    public int RefreshTokenValidityInDays { get; init; } = 5;
}