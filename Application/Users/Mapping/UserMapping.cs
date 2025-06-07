using Application.Users.Commands;
using Application.Users.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<CreateUserCommand, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<UpdateUserCommand, UpdateUserDto>().ReverseMap();
        }
    }
}
