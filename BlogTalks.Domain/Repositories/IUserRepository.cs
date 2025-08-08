using BlogTalks.Domain.Entities;

namespace BlogTalks.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? Login(string username, string password);

    User? Register(string username, string password, string name);
}