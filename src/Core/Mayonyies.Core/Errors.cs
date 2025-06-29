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

    public static class User
    {
        public static Error UsernameOrPasswordDoesntMatch() =>
            new("USR-0001",
                "Username or password doesn't match."
            );

        public static Error AccountIsNotActive() =>
            new("USR-0002",
                "Account is not active. Please contact support to activate your account."
            );
        
        public static Error TokenIsInvalid() =>
            new("USR-0003",
                "Token is invalid. Please login again."
            );
    }
}