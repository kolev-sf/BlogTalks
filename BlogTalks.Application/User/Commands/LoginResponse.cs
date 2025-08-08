namespace BlogTalks.Application.User.Commands;

public record LoginResponse(string Token, string RefreshToken, int UserId);