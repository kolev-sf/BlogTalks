using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using Infrastructure.Database;

namespace BlogTalks.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public User? GetByUsername(string username)
    {
        return _dbSet.FirstOrDefault(x => x.Username.Equals(username));
    }

    public IEnumerable<User> GetUsersByIds(IEnumerable<int> ids)
    {
        return _dbSet.Where(u => ids.Contains(u.Id));
    }
}