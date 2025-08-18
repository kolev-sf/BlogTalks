using BlogTalks.Application.BlogPost.Queries;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using Moq;
using Xunit;

namespace BlogTalks.Tests.UnitTests.BlogPosts;

public class GetBlogPostByIdHandlerTests
{
    private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
    private readonly GetByIdHandler _handler;

    public GetBlogPostByIdHandlerTests()
    {
        _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
        _handler = new GetByIdHandler(_blogPostRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_BlogPostExists_ReturnsBlogResponse()
    {
        // Arrange
        var expectedId = 1;
        var expectedBlogPost = new BlogPost { Id = expectedId, Title = "Title", Text = "Text", Tags = new List<string> { "Tag1" }, CreatedAt = DateTime.Now, CreatedBy = 5 };

        _blogPostRepositoryMock
            .Setup(repo => repo.GetById(expectedId))
            .Returns(expectedBlogPost);

        var query = new GetByIdRequest { Id = expectedId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedId, result.Id);
        Assert.Equal(expectedBlogPost.Title, result.Title);
    }

    [Fact]
    public async Task Handle_BlogPostDoesNotExist_ReturnsException()
    {
        // Arrange
        var expectedId = 125;
        var query = new GetByIdRequest { Id = expectedId };

        // Act
        // Assert
        await Assert.ThrowsAsync<BlogTalks.Domain.Exceptions.BlogTalksException>(() => _handler.Handle(query, CancellationToken.None));
    }
}