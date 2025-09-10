using BlogTalks.Application.User.Commands;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using Xunit;

namespace BlogTalks.Tests.UnitTests.User;

public class RegisterHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<LoginHandler>> _loggerMock;
    private readonly RegisterHandler _handler;

    public RegisterHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<LoginHandler>>();
        _handler = new RegisterHandler(_userRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsRegisterResponse()
    {
        // Arrange
        var request = new RegisterRequest("newuser", "password123", "Test User");
        _userRepositoryMock
            .Setup(r => r.GetByUsername(request.Username))
            .Returns((Domain.Entities.User?)null);

        _userRepositoryMock
            .Setup(r => r.Add(It.IsAny<Domain.Entities.User>()))
            .Callback<Domain.Entities.User>(u => u.Id = 42);

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(42, result.Id);
        _userRepositoryMock.Verify(r => r.Add(It.Is<Domain.Entities.User>(u =>
            u.Username == request.Username &&
            u.Name == request.Name
        )), Times.Once);
    }

    [Fact]
    public async Task Handle_UserAlreadyExists_ThrowsBlogTalksException()
    {
        // Arrange
        var request = new RegisterRequest("newuser", "password123", "Test User");

        _userRepositoryMock
            .Setup(r => r.GetByUsername(request.Username))
            .Returns(new Domain.Entities.User { Username = request.Username });

        // Act & Assert
        var ex = await Assert.ThrowsAsync<BlogTalksException>(() => _handler.Handle(request, default));
        Assert.Equal(HttpStatusCode.BadRequest, ex.StatusCode);
        Assert.Contains("exist", ex.Message, System.StringComparison.OrdinalIgnoreCase);
    }
}