using System.Net;
using BlogTalks.Application.Abstractions;
using BlogTalks.Application.User.Commands;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Shared;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BlogTalks.Tests.UnitTests.User;

public class LoginHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly Mock<ILogger<LoginHandler>> _loggerMock;
    private readonly LoginHandler _handler;

    public LoginHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _authServiceMock = new Mock<IAuthService>();
        _loggerMock = new Mock<ILogger<LoginHandler>>();
        _handler = new LoginHandler(_userRepositoryMock.Object, _authServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCredentials_ReturnsLoginResponse()
    {
        // Arrange
        var user = new Domain.Entities.User
        {
            Id = 1,
            Username = "testuser",
            Password = PasswordHasher.HashPassword("password123"),
            Name = "Test User"
        };
        var request = new LoginRequest("testuser", "password123");

        _userRepositoryMock
            .Setup(r => r.GetByUsername(request.Username))
            .Returns(user);

        _authServiceMock
            .Setup(a => a.CreateToken(user.Id, user.Name, user.Username, It.IsAny<IList<string>>()))
            .Returns("token123");

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("token123", result.Token);
        Assert.Equal(user.Id, result.UserId);
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_ThrowsBlogTalksException()
    {
        // Arrange
        var request = new LoginRequest("nouser", "password123");
        _userRepositoryMock
            .Setup(r => r.GetByUsername(request.Username))
            .Returns((Domain.Entities.User?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<BlogTalksException>(() => _handler.Handle(request, default));
        Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        Assert.Contains("does not match", ex.Message, System.StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Handle_InvalidPassword_ThrowsBlogTalksException()
    {
        // Arrange
        var user = new Domain.Entities.User
        {
            Id = 1,
            Username = "testuser",
            Password = PasswordHasher.HashPassword("correctpassword"),
            Name = "Test User"
        };
        var request = new LoginRequest("testuser", "wrongpassword");

        _userRepositoryMock
            .Setup(r => r.GetByUsername(request.Username))
            .Returns(user);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<BlogTalksException>(() => _handler.Handle(request, default));
        Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        Assert.Contains("does not match", ex.Message, System.StringComparison.OrdinalIgnoreCase);
    }
}