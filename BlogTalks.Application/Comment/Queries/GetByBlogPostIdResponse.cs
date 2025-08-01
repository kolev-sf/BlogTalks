namespace BlogTalks.Application.Comment.Queries;

public class GetByBlogPostIdResponse
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public string CreatorName { get; set; } = string.Empty;
}