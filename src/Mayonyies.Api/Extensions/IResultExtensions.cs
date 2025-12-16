using Mayonyies.Core;
using Mayonyies.Core.Shared;

namespace Mayonyies.Api.Extensions;

public static class IResultExtensions
{
    public static IResult EvaluateError(this Microsoft.AspNetCore.Http.IResultExtensions _, Error error)
    {
        if (error == Errors.User.TokenIsInvalid() || error == Errors.User.UsernameOrPasswordDoesntMatch())
            return TypedResults.Unauthorized();

        return TypedResults.BadRequest(error);
    }
}