using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.User.Commands;

public class RegisterHandler : IRequestHandler<RegisterRequest, RegisterResponse>
{
    private readonly IUserRepository _userRepository;

    public RegisterHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.Register(request.Username, request.Password, request.Name);
        if (user == null) {
            // Handle the case where registration fails, e.g., user already exists
            throw new InvalidOperationException("User registration failed.");
        }

        // return Id of created Comment Entity
        return new RegisterResponse(user.Id);
    }
}