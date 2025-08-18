using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using Infrastructure.Database;

namespace BlogTalks.Infrastructure.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context) { }

    IEnumerable<Comment> ICommentRepository.GetCommentsByBlogPostId(int blogPostId)
    {
        return _dbSet.Where(c => c.BlogPostId == blogPostId);
    }
}