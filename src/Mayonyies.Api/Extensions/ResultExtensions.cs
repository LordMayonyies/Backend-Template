using Mayonyies.Core;
using Mayonyies.Core.Shared;

namespace Mayonyies.Api.Extensions;

public static class ResultExtensions
{
    public static IResult EvaluateError(this IResultExtensions result, Error error)
    {
        if (error == Errors.User.TokenIsInvalid() || error == Errors.User.UsernameOrPasswordDoesntMatch())
            return TypedResults.Unauthorized();

        return TypedResults.BadRequest(error);
    }
}