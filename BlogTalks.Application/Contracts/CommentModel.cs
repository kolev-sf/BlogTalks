namespace BlogTalks.Application.Contracts;

public class GetCommentModel
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int BlogPostId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public string CreatorName { get; set; } = string.Empty;
}
