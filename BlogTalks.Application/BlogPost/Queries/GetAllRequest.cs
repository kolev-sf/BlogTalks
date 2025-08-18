using MediatR;

namespace BlogTalks.Application.BlogPost.Queries;

public record GetAllRequest(
    string? SearchWord,
    string? Tag,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<GetAllResponse>;
