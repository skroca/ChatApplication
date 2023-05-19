using Data.Models;

namespace Data.Repositories
{
    public interface ILoginRepository
    {
        public User? GetUserByCredentials(string email, string password);
    }
}