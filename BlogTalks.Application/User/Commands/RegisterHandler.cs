using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BlogTalks.Application.User.Commands;

public class RegisterHandler : IRequestHandler<RegisterRequest, RegisterResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<LoginHandler> _logger;

    public RegisterHandler(IUserRepository userRepository, ILogger<LoginHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RegisterRequest for user: {Username}", request.Username);

        // Check if the user already exists
        var existingUser = _userRepository.GetByUsername(request.Username);
        if (existingUser != null)
            throw new BlogTalksException("User exist.", HttpStatusCode.BadRequest);

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