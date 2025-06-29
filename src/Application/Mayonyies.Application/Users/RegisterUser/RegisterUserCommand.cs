namespace Mayonyies.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Username, string Password) : ICommand;