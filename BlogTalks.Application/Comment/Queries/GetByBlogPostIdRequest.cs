using BlogTalks.Application.Comment.Queries;
using MediatR;

namespace BlogTalks.Application.BlogPost.Queries;

public record GetByBlogPostIdRequest(int blogPostId) : IRequest<List<GetByBlogPostIdResponse>>;
