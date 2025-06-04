using AutoMapper;
using Bookify_Library_mgnt.Dtos.Books;
using Domain.Entities;

namespace Bookify_Library_mgnt.Mappings
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();

        }
    }
}
