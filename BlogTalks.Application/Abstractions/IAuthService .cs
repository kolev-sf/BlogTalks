namespace BlogTalks.Application.Abstractions
{
    public interface IAuthService
    {
        public string CreateToken(int userId, string username, string email, IList<string> roles);
    }
}
