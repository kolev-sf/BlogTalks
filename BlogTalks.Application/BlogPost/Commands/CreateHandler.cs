using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogTalks.Application.BlogPost.Commands;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateHandler(IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor)
    {
        _blogPostRepository = blogPostRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var blogPost = new Domain.Entities.BlogPost
        {
            Title = request.Title,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = Convert.ToInt32(userId),
            Tags = request.Tags
        };

        // add insert logic here
        _blogPostRepository.Add(blogPost);

        // return Id of created Comment Entity
        return new CreateResponse(blogPost.Id);
    }
}
