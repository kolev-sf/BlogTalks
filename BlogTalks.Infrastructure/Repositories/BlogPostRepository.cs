using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using Infrastructure.Database;

namespace BlogTalks.Infrastructure.Repositories;

public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
{
    public BlogPostRepository(ApplicationDbContext context) : base(context) { }

    public BlogPost? GetBlogPostByName(string name)
    {
        return _dbSet.FirstOrDefault(c => c.Title.Equals(name));
    }

    public (int count, List<BlogPost> list) GetPagedAsync(int pageNumber, int pageSize, string? searchWord, string? tag)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchWord))
            query = query.Where(bp => bp.Title.Contains(searchWord, StringComparison.InvariantCultureIgnoreCase) || bp.Text.Contains(searchWord, StringComparison.InvariantCultureIgnoreCase));

        if (!string.IsNullOrWhiteSpace(tag))
            query = query.Where(bp => bp.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase));

        var count = query.Count();
        var list = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return (count, list);
    }
}