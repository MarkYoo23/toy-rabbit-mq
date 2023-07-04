using API.Domains.Users;
using MediatR;

namespace API.Presentations.Commands;

public class AddNewUserCommand : IRequest<User>
{
    public string Name { get; set; }
}
