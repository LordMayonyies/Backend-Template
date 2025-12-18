using Mayonyies.Api.Extensions;
using Mayonyies.Application.Authentication.LogIn;
using Mayonyies.Application.Authentication.RefreshToken;
using Mayonyies.Application.Messaging;

namespace Mayonyies.Api.Authentication;

internal static class AuthenticationEndpoints
{
    internal static IEndpointRouteBuilder MapAuthentication(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var authenticationGroup = endpointRouteBuilder
            .MapGroup("/authentication")
            .AllowAnonymous()
            .WithTags("Authentication");

        authenticationGroup.MapPost("logIn",
            async (
                LogInRequest request,
                ICommandHandler<LogInCommand, LogInCommandResponse> commandHandler,
                CancellationToken cancellationToken
            ) =>
            {
                var command = new LogInCommand(request.Username, request.Password);

                var response = await commandHandler.Handle(command, cancellationToken);

                return response.IsFailure
                    ? Results.Extensions.EvaluateError(response.Error)
                    : TypedResults.Ok(response.Value);
            });

        authenticationGroup.MapPost("refreshToken", RefreshToken);

        return endpointRouteBuilder;
    }

    private static async Task<IResult> RefreshToken(
        RefreshTokenRequest request,
        ICommandHandler<RefreshTokenCommand, RefreshTokenCommandResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(request.AccessToken, request.RefreshToken);

        var result = await commandHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? Results.Extensions.EvaluateError(result.Error)
            : Results.Ok(result.Value);
    }
}