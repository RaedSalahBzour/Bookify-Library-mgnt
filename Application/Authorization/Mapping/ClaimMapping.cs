using Application.Authorization.Commands.Claims;
using Application.Authorization.Dtos.Claims;
using AutoMapper;

namespace Application.Authorization.Mapping;

public class ClaimMapping : Profile
{
    public ClaimMapping()
    {
        CreateMap<AddClaimToUserDto, AddClaimToUserCommand>().ReverseMap();
    }


}
