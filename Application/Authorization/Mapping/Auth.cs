using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
using Application.Users.Dtos;
using AutoMapper;

namespace Application.Authorization.Mapping;

public class Auth : Profile
{
    public Auth()
    {
        CreateMap<LoginDto, LoginCommand>().ReverseMap();
        CreateMap<RefreshTokenRequestDto, RefreshTokenCommand>().ReverseMap();
    }
}
