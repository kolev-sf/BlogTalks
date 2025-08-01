namespace BlogTalks.Application.BlogPost.Queries;

public class GetByIdResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new List<string>();
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public string CreatorName { get; set; } = string.Empty;
}