namespace Mayonyies.Core.Shared;

public readonly record struct Error(string Code, string Message)
{
    private const string Separator = ": ";
    public string Serialize() => $"{Code}{Separator}{Message}";
    
    public static Error Deserialize(string serialized) =>
        serialized.Split(Separator, 2) switch
        {
            [var code, var message] => new Error(code, message),
            _ => throw new ArgumentException("Invalid serialized error format.", nameof(serialized))
        };
}