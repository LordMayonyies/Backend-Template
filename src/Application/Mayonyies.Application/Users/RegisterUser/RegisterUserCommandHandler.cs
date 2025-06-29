namespace Mayonyies.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    public Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}