using BlogTalks.Domain.Entities;

namespace BlogTalks.Domain.Repositories;

public interface IBlogPostRepository : IRepository<BlogPost>
{
    BlogPost? GetBlogPostByName(string name);

    public (int count, List<BlogPost> list) GetPagedAsync(int pageNumber, int pageSize, string? searchWord,
        string? tag);
}
