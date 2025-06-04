using Application.Books.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Books.Mappings
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
