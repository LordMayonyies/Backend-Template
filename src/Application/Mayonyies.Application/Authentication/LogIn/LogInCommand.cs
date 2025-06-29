using Mayonyies.Application.Messaging;

namespace Mayonyies.Application.Authentication.LogIn;

public sealed record LogInCommand(string Username, string Password) : ICommand<LogInCommandResponse>;