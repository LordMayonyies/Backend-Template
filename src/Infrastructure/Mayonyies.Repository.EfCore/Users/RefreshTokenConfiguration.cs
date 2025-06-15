using Mayonyies.Core.Users;

namespace Mayonyies.Repository.EfCore.Users;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        
        builder.HasKey(refreshToken => new { refreshToken.UserId, refreshToken.Value });
        
        builder.Property(regrefreshToken => regrefreshToken.UserId)
            .HasMaxLength(User.IdTextMaxLength);
        
        builder.Property(refreshToken => refreshToken.Value);
    }
}