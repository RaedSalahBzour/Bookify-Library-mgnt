using Application.Authorization.Commands.Roles;
using Application.Authorization.Dtos.Roles;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<IdentityRole, RoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));

        CreateMap<IdentityRole, CreateRoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));
        CreateMap<IdentityRole, UpdateRoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));
        CreateMap<CreateRoleDto, CreateRoleCommand>().ReverseMap();
        CreateMap<UpdateRoleDto, UpdateRoleCommand>().ReverseMap();

    }
}
