using AutoMapper;
using Bookify_Library_mgnt.Dtos.Users;
using Domain.Entities;

namespace Bookify_Library_mgnt.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
        }
    }
}
