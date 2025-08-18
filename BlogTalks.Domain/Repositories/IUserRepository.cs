using BlogTalks.Domain.Entities;

namespace BlogTalks.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? GetByUsername(string username);

    public IEnumerable<User> GetUsersByIds(IEnumerable<int> ids);
}