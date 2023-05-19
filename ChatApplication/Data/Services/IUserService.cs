using Data.DTOModels;

namespace Data.Services
{
    public interface IUserService
    {
        public UserDTO AddUser(UserDTO userDTO);
    }
}