using FluentValidation;

namespace Mayonyies.Application.Authentication.LogIn;

internal sealed class LogInCommandValidator : AbstractValidator<LogInCommand>
{
    public LogInCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}