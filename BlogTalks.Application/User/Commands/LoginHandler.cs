using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.User.Commands;

public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUserRepository _userRepository;

    public LoginHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.Login(request.Username, request.Password);
        if (user == null)
        {
            // Handle the case where registration fails, e.g., user already exists
            //throw new InvalidOperationException("User login failed.");
            return null;
        }

        // return Id of created Comment Entity
        return new LoginResponse("", "", user.Id);
    }
}