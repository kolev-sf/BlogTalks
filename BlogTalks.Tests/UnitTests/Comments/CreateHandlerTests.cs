using System.Net;
using System.Security.Claims;
using BlogTalks.Application.Comment.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BlogTalks.Tests.UnitTests.Comments;

public class CreateHandlerTests
{
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<CreateHandler>> _loggerMock;
    private readonly CreateHandler _handler;

    public CreateHandlerTests()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<CreateHandler>>();
        _handler = new CreateHandler(
            _commentRepositoryMock.Object,
            _httpContextAccessorMock.Object,
            _blogPostRepositoryMock.Object,
            _userRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreateResponse()
    {
        // Arrange
        var userId = "42";
        var email = "user@email.com";
        var name = "Test User";
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, name)
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(c => c.User).Returns(principal);
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

        var blogPost = new BlogPost
        {
            Id = 10,
            Title = "Blog Title",
            Text = "Blog Content",
            CreatedBy = 99,
            Tags = new List<string> { "tag1" }
        };

        _blogPostRepositoryMock
            .Setup(r => r.GetById(blogPost.Id))
            .Returns(blogPost);

        var user = new Domain.Entities.User { Id = 99, Username = "author@email.com", Name = "Author" };
        _userRepositoryMock
            .Setup(r => r.GetById(blogPost.CreatedBy))
            .Returns(user);

        var request = new CreateRequest("Test comment", blogPost.Id);

        _commentRepositoryMock
            .Setup(r => r.Add(It.IsAny<Comment>()))
            .Callback<Comment>(c => c.Id = 123);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(123, result.Id);
        _commentRepositoryMock.Verify(r => r.Add(It.Is<Comment>(c =>
            c.BlogPostId == blogPost.Id &&
            c.Text == request.Text &&
            c.CreatedBy == int.Parse(userId)
        )), Times.Once);
    }

    [Fact]
    public async Task Handle_BlogPostDoesNotExist_ThrowsBlogTalksException()
    {
        // Arrange
        var userId = "42";
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(c => c.User).Returns(principal);
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

        _blogPostRepositoryMock
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns((BlogPost?)null);

        var request = new CreateRequest("Test comment", 9999);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<BlogTalksException>(() => _handler.Handle(request, CancellationToken.None));
        Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        Assert.Contains("not found", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
}