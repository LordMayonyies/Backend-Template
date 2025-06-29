using Mayonyies.Core.Users;

namespace Mayonyies.Repository.EfCore.Users;

internal sealed class UserRepository(MayonyiesDbContext dbContext) : Repository<User, int>(dbContext), IUserRepository
{
    
}