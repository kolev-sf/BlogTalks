using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Shared;
using Infrastructure.Database;

namespace BlogTalks.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public User? Login(string username, string password)
    {
        var user = _dbSet.FirstOrDefault(x => x.Username.Equals(username));
        if (user == null)
        {
            return null; // Return null if username or password is empty
        }

        var passwordConfirmed = PasswordHasher.VerifyPassword(password, user.Password);
        if (!passwordConfirmed)
        {
            return null; // Return null if password verification fails
        }

        return user;
    }

    public User? Register(string username, string password, string name)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return null; // Return null if username or password is empty
        }

        var user = new Domain.Entities.User
        {
            Username = username,
            Password = PasswordHasher.HashPassword(password),
            Name = name,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 5, // not implemented yet, TODO
        };

        _dbSet.Add(user);
        _context.SaveChanges(); // Save changes to the database

        return user;
    }
}