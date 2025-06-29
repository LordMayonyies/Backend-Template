using Mayonyies.Api.Extensions;
using Mayonyies.Application.Authentication.LogIn;
using Mayonyies.Application.Authentication.RefreshToken;
using Mayonyies.Application.Messaging;
using Mayonyies.Core.Shared;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Mayonyies.Api.Endpoints.Authentication;

internal static class AuthenticationEndpoints
{
    internal static IEndpointRouteBuilder MapAuthentication(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var authenticationGroup = endpointRouteBuilder
            .MapGroup("/authentication")
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

    private static IResult RefreshToken(
        RefreshTokenCommand request,
        ICommandHandler<RefreshTokenCommand, RefreshTokenCommandResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        var response = commandHandler.Handle(request, cancellationToken);

        return Results.Ok(response);
    }
}

public sealed record LogInRequest(string Username, string Password);