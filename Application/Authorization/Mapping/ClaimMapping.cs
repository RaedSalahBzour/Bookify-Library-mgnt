using Application.Authorization.Commands.Claims;
using Application.Authorization.Dtos.Claims;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Mapping
{
    public class ClaimMapping : Profile
    {
        public ClaimMapping()
        {
            CreateMap<AddClaimToUserDto, AddClaimToUserCommand>().ReverseMap();
        }


    }
}
