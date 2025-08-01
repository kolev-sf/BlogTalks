using MediatR;

namespace BlogTalks.Application.BlogPost.Queries;

public record GetAllRequest() : IRequest<List<GetAllResponse>>;
