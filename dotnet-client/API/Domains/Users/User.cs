using API.Domains.SeedWorks;

namespace API.Domains.Users;

public class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}
