namespace BlogTalks.Application.Comment.Queries;

public class GetByIdResponse
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
}