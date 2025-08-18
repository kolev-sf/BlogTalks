using System.Net;
using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BlogTalks.Domain.Exceptions;

namespace BlogTalks.Application.Comment.Commands;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBlogPostRepository _blogPostRepository;

    public CreateHandler(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IBlogPostRepository blogPostRepository)
    {
        _commentRepository = commentRepository;
        _httpContextAccessor = httpContextAccessor;
        _blogPostRepository = blogPostRepository;
    }

    public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var blogPost = _blogPostRepository.GetById(request.BlogPostId);

        if (blogPost == null)
            throw new BlogTalksException($"Blog post with ID {request.BlogPostId} not found.", HttpStatusCode.NotFound);

        var comment = new Domain.Entities.Comment
        {
            BlogPostId = request.BlogPostId,
            BlogPost = blogPost,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = Convert.ToInt32(userId),
        };

        // add insert logic here
        _commentRepository.Add(comment);

        // return Id of created Comment Entity
        return new CreateResponse {Id = comment.Id};
    }
}
