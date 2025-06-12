using Mayonyies.Core.Shared;

namespace Mayonyies.Core;

public static class Errors
{
    public static class Validation
    {
        public static Error Create(IEnumerable<Error> errors) =>
            new Error(
                "ValidationError",
                string.Join(" || ", errors.Select(error => error.Serialize()))
            );
    }
}