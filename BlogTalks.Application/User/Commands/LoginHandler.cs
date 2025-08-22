using BlogTalks.Application.Abstractions;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BlogTalks.Application.User.Commands;

public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(IUserRepository userRepository, IAuthService authService, ILogger<LoginHandler> logger)
    {
        _userRepository = userRepository;
        _authService = authService;
        _logger = logger;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling LoginRequest for user: {Username}", request.Username);
        var user = _userRepository.GetByUsername(request.Username);
        if (user == null)
            throw new BlogTalksException("User and password does not match.", HttpStatusCode.NotFound);

        var passwordConfirmed = PasswordHasher.VerifyPassword(request.Password, user.Password);
        if (!passwordConfirmed)
            throw new BlogTalksException("User and password does not match.", HttpStatusCode.NotFound);

        var token = _authService.CreateToken(user.Id, user.Name, user.Username, new List<string>());
        return new LoginResponse(token, "", user.Id);
    }
}