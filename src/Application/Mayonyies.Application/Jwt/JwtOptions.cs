namespace Mayonyies.Application.Jwt;

public record JwtOptions(string Issuer, string Audience, string SecretKey, int AccessTokenValidityInMinutes, int RefreshTokenValidityInDays)
{
    public const string SectionName = "JwtOptions";
}