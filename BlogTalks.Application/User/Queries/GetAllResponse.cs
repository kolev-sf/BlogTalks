namespace BlogTalks.Application.User.Queries;

public class GetAllResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new List<string>();
    public string CreatorName { get; set; } = string.Empty;
}