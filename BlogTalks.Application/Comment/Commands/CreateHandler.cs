using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BlogTalks.Application.Comment.Commands;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateHandler> _logger;

    public CreateHandler(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IBlogPostRepository blogPostRepository, IUserRepository userRepository, ILogger<CreateHandler> logger)
    {
        _commentRepository = commentRepository;
        _httpContextAccessor = httpContextAccessor;
        _blogPostRepository = blogPostRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateRequest for Comment on BlogPost with ID: {BlogPostId}", request.BlogPostId);

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
        var name = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
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

        var user = _userRepository.GetById(blogPost.CreatedBy);
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7055");

        var dto = new
        {
            From = email,
            To = user!.Username,
            Subject = "New Comment Added",
            Body = $"A new comment has been added to the blog post '{blogPost.Title}' by {name}."
        };

        var response = await httpClient.PostAsJsonAsync("/SendEmail", dto, cancellationToken);

        // return Id of created Comment Entity
        return new CreateResponse {Id = comment.Id};
    }
}
