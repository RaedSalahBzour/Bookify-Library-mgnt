using AutoMapper;
using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Mappings
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Book, CreateBookDto>().ReverseMap()
                .ForMember(dest => dest.CategoryBooks, opt => opt.Ignore()); ;
            CreateMap<Book, UpdateBookDto>().ReverseMap()
                .ForMember(dest => dest.CategoryBooks, opt => opt.Ignore()); ;
            CreateMap<Book, BooksDto>().ReverseMap()
                .ForMember(dest => dest.CategoryBooks, opt => opt.Ignore()); ;
        }
    }
}
