using MediatR;

namespace BlogTalks.Application.User.Queries;

public record GetAllRequest() : IRequest<List<GetAllResponse>>;
