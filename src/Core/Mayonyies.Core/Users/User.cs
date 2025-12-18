namespace Mayonyies.Core.Users;

public sealed class User : Entity<int>, IAggregateRoot
{
    private readonly List<RefreshToken> _refreshTokens;

    private User()
    {
        Username = null!;
        Email = null!;
        PasswordHash = null!;
        IsActive = true;
        _refreshTokens = [];
    }

    public User(string username, string email)
        : this()
    {
        Username = username;
        Email = email;
    }

    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens;

    public Result<RefreshToken> AddRefreshToken(TimeSpan expiryTime)
    {
        var expireAtUtc = DateTime.UtcNow.Add(expiryTime);
        var refreshToken = RefreshToken.Create(this, expireAtUtc);

        _refreshTokens.Add(refreshToken);

        return refreshToken;
    }

    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        _refreshTokens.Remove(refreshToken);
    }

    public Result SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            PasswordHash = passwordHash;

        return Result.Success();
    }
}