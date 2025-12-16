namespace Mayonyies.Application.Jwt;

public sealed record JwtOptions
{
    public const string SectionName = "Jwt";

    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; }
    public int AccessTokenValidityInMinutes { get; init; } = 5;
    public int RefreshTokenValidityInDays { get; init; } = 5;
}