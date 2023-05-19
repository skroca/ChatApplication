using Data.Models;
using Microsoft.Extensions.Configuration;

namespace Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ChatApplicationDBContext context;

        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            context = new ChatApplicationDBContext(_configuration);
        }
        public User? GetUserByCredentials(string email, string password)
        {
            return context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
