using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using Infrastructure.Database;

namespace BlogTalks.Infrastructure.Repositories
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context) { }

        public BlogPost? GetBlogPostByName(string name)
        {
            return _dbSet.FirstOrDefault(c => c.Title.Equals(name));
        }
    }
}
