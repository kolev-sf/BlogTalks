namespace BlogTalks.Application.User.Queries;

public class GetByIdResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}