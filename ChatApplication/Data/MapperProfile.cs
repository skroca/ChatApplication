using AutoMapper;
using Data.DTOModels;
using Data.Models;

namespace Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
            
    }
}