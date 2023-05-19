namespace Data.Services
{
    public interface ILoginService
    {
        public bool Login(string email, string password);
    }
}