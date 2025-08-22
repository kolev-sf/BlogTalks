using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BlogTalks.Application.BlogPost.Commands;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CreateHandler> _logger;

    public CreateHandler(IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor, ILogger<CreateHandler> logger)
    {
        _blogPostRepository = blogPostRepository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateRequest for BlogPost with Title: {Title}", request.Title);

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
