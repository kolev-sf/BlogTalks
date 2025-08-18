using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using MediatR;
using System.Net;

namespace BlogTalks.Application.Comment.Queries;

public class GetByBlogPostIdHandler : IRequestHandler<GetByBlogPostIdRequest, List<GetByBlogPostIdResponse>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IBlogPostRepository _blogPostRepository;

    public GetByBlogPostIdHandler(ICommentRepository commentRepository, IBlogPostRepository blogPostRepository)
    {
        _commentRepository = commentRepository;
        _blogPostRepository = blogPostRepository;
    }

    public async Task<List<GetByBlogPostIdResponse>> Handle(GetByBlogPostIdRequest request, CancellationToken cancellationToken)
    {
        // get list of BlogPostEntities
        var blogPost = _blogPostRepository.GetById(request.BlogPostId);
        if (blogPost == null)
            throw new BlogTalksException($"Blog post with Id {request.BlogPostId} not found.", HttpStatusCode.NotFound);

        var list = _commentRepository.GetCommentsByBlogPostId(request.BlogPostId);

        // map list to GetAllResponse
        var getAllResponseList = new List<GetByBlogPostIdResponse>();
        foreach (var item in list)
        {
            var response = new GetByBlogPostIdResponse
            {
                Id = item.Id,
                Text = item.Text,
                CreatorName = "XXX", // not implemted yet, TODO
                CreatedAt = item.CreatedAt,
                CreatedBy = item.CreatedBy,
            };
            getAllResponseList.Add(response);
        }

        return getAllResponseList;
    }
}
