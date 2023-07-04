using API.Domains.Users;
using API.Presentations.Commands;
using MediatR;

namespace API.Applications.Services.Users;

public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, User>
{
    private readonly IUserRepository _userRepository;

    public AddNewUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { Name = request.Name };
        var userEntry = await _userRepository.AddAsync(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken, 1);

        return userEntry.Entity;
    }
}
