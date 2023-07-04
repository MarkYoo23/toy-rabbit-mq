using API.Domains.Users;
using API.Infrastructures.Contexts;
using API.Infrastructures.SeedWorks;

namespace API.Infrastructures.Repositories;

public class UserRepository : CommandRepository<User>, IUserRepository
{
    public UserRepository(UserContext context) : base(context)
    {
    }
}
