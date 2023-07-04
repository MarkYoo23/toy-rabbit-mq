
using API.Domains.Users;
using API.Infrastructures.EntityConfigurations;
using API.Infrastructures.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructures.Contexts;

public class UserContext : BaseContext
{
    private DbSet<User> Users { get; set; } = null!;

    public UserContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfig());
    }
}
