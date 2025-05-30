﻿using AutoMapper;
using Bookify_Library_mgnt.Dtos.Roles;
using Microsoft.AspNetCore.Identity;

namespace Bookify_Library_mgnt.Mappings
{
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

        }
    }
}
