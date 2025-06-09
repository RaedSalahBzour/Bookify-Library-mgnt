using Application.Books.Commands;
using Application.Books.Dtos;
using AutoMapper;
using Data.Entities;

namespace Application.Books.Mappings
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<CreateBookDto, CreateBookCommand>().ReverseMap();
            CreateMap<UpdateBookDto, UpdateBookCommand>().ReverseMap();

        }
    }
}
