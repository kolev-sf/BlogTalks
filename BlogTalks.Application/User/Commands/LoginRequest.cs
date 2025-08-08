using MediatR;

namespace BlogTalks.Application.User.Commands;

public record LoginRequest(string Username, string Password) : IRequest<LoginResponse>;