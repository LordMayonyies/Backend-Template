namespace Mayonyies.Application.Jwt;

public sealed record JwtOptions(
    string Issuer,
    string Audience,
    string SecretKey,
    int AccessTokenValidityInMinutes,
    int RefreshTokenValidityInDays)
{
    public const string SectionName = "Jwt";
}