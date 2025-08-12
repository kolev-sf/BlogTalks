using System.Reflection.Metadata.Ecma335;
using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Shared;
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
        // Check if the user already exists
        var existingUser = _userRepository.GetByUsername(request.Username);
        if (existingUser != null)
        {
            return null;
        }

        var user = new Domain.Entities.User
        {
            Username = request.Username,
            Password = PasswordHasher.HashPassword(request.Password),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 0 // registered by system or admin, adjust as needed
        };

        _userRepository.Add(user);

        return new RegisterResponse(user.Id);
    }
}