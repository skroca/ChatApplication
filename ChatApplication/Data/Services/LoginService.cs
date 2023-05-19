using Data.Repositories;
using Data.Utils;

namespace Data.Services
{
    public class LoginService : ILoginService
    {
        public readonly ILoginRepository _loginRepository;
       
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public bool Login(string email, string password)
        {
            var user = _loginRepository.GetUserByCredentials(email, Encrypt.GetSHA256(password));

            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
