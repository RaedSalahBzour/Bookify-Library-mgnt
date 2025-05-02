using AutoMapper;
using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        }
    }
}
