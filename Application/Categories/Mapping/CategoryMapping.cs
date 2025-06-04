using Application.Categories.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Categories.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Books,
                opt => opt.MapFrom(src => src.CategoryBooks.Select(cb => cb.Book))).ReverseMap();
        }
    }
}
