using Data.Models;
using Microsoft.Extensions.Configuration;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ChatApplicationDBContext context;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            context = new ChatApplicationDBContext(_configuration);
        }
        public User AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
    }
}
