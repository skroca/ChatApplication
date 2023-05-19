using Data.Models;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        public User AddUser(User user);
    }
}