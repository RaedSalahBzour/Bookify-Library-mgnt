using Application.Authorization.Commands.Auth;
using Application.Authorization.Dtos.Token;
using Application.Users.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Mapping
{
    public class Auth : Profile
    {
        public Auth()
        {
            CreateMap<LoginDto, LoginCommand>().ReverseMap();
            CreateMap<RefreshTokenRequestDto, RefreshTokenCommand>().ReverseMap();
        }
    }
}
