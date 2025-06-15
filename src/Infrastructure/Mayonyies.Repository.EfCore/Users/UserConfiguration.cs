using Mayonyies.Core.Users;


namespace Mayonyies.Repository.EfCore.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedOnAdd();

        builder.Property(user => user.Username)
            .HasMaxLength(User.IdTextMaxLength);

        builder.Property(user => user.Email)
            .HasMaxLength(User.ShortTextMaxLength);

        builder.Property(user => user.PasswordHash);

        builder.Property(user => user.IsActive);

        builder.HasMany(user => user.RefreshTokens)
            .WithOne()
            .HasPrincipalKey(user => user.Id)
            .HasForeignKey(refreshToken => refreshToken.UserId);
    }
}