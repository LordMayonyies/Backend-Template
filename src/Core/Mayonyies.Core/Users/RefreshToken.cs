namespace Mayonyies.Core.Users;

public sealed class RefreshToken : Entity
{
    private RefreshToken()
    {
        ExpiresAtUtc = DateTime.MinValue;
    }

    private RefreshToken(User user, Guid value, DateTime expiresAtUtc)
        : this()
    {
        UserId = user.Id;
        Value = value;
        ExpiresAtUtc = expiresAtUtc;
    }

    public int UserId { get; private init; }
    public Guid Value { get; private init; }
    public DateTime ExpiresAtUtc { get; private set; }
    
    public static RefreshToken Create(User user, DateTime expiresAtUtc) =>
        new (user, Guid.CreateVersion7(), expiresAtUtc);
    
    public override bool Equals(Entity? entityBase)
    {
        if (entityBase is not RefreshToken other)
            return false;
        
        return UserId == other.UserId && Value == other.Value;
    }

    public override int GetHashCode() =>
        HashCode.Combine(GetType(), UserId, Value);
}