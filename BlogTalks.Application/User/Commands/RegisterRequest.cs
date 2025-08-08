using MediatR;

namespace BlogTalks.Application.User.Commands;

public record RegisterRequest(string Username, string Password, string Name) : IRequest<RegisterResponse>;