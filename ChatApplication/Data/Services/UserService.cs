using AutoMapper;
using Data.DTOModels;
using Data.Models;
using Data.Repositories;
using Data.Utils;

namespace Data.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        public readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public UserDTO AddUser(UserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            user.Password = Encrypt.GetSHA256(user.Password);
            user.DateCreated = DateTime.Now;
          
            userDTO = _mapper.Map<UserDTO>(_userRepository.AddUser(user));
            return userDTO;
        }
    }
}
