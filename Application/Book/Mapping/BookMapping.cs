using Application.Book.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Book.Mappings
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
