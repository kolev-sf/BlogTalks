using System.Security.Claims;
using BlogTalks.Application.BlogPost.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace BlogTalks.Tests.UnitTests.BlogPosts;

public class CreateHandlerTests
{
    private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly CreateHandler _handler;

    public CreateHandlerTests()
    {
        _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _handler = new CreateHandler(_blogPostRepositoryMock.Object, _httpContextAccessorMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreateResponse()
    {
        // Arrange
        var userId = "42";
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(c => c.User).Returns(principal);
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

        var request = new CreateRequest("Test Title", "Test Content", new List<string> { "tag1", "tag2" });


        // Simulate repository Add sets Id
        _blogPostRepositoryMock
            .Setup(r => r.Add(It.IsAny<BlogPost>()))
            .Callback<BlogPost>(b => b.Id = 123);

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(123, result.Id);
        _blogPostRepositoryMock.Verify(r => r.Add(It.Is<BlogPost>(b =>
            b.Title == request.Title &&
            b.Text == request.Text &&
            b.CreatedBy == int.Parse(userId) &&
            b.Tags == request.Tags
        )), Times.Once);
    }
}