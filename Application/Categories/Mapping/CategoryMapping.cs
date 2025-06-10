using Application.Categories.Commands;
using Application.Categories.Dtos;
using AutoMapper;
using Data.Entities;

namespace Application.Categories.Mapping;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, CategoryDto>()
        .ForMember(dest => dest.Books,
            opt => opt.MapFrom(src => src.CategoryBooks.Select(cb => cb.Book))).ReverseMap();
        CreateMap<UpdateCategoryCommand, UpdateCategoryDto>().ReverseMap();
        CreateMap<CreateCategoryCommand, CreateCategoryDto>().ReverseMap();

    }
}
