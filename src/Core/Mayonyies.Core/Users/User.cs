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

    public User(string username, string email, string passwordHash)
        : this()
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }
    
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens;
    
    public Result AddRefreshToken(DateTime expiresAtUtc)
    {
        var refreshToken = RefreshToken.Create(this, expiresAtUtc);

        _refreshTokens.Add(refreshToken);
        
        return Result.Success();
    }
    
    public override int GetHashCode() =>
        HashCode.Combine(GetType(), Id);
}