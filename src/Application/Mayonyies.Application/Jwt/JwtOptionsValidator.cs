using Microsoft.Extensions.Options;

namespace Mayonyies.Application.Jwt;

internal sealed class JwtOptionsValidator : IValidateOptions<JwtOptions>
{
    public ValidateOptionsResult Validate(string? name, JwtOptions options)
    {
        var failures = new List<string>();

        if (string.IsNullOrWhiteSpace(options.Audience))
            failures.Add($"'{JwtOptions.SectionName}:{nameof(JwtOptions.Audience)}' must have a value.");
        
        if (string.IsNullOrWhiteSpace(options.Issuer))
            failures.Add($"'{JwtOptions.SectionName}:{nameof(JwtOptions.Issuer)}' must have a value.");
        
        if (string.IsNullOrWhiteSpace(options.SecretKey))
            failures.Add($"'{JwtOptions.SectionName}:{nameof(JwtOptions.SecretKey)}' must have a value.");

        return failures.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(failures);
    }
}