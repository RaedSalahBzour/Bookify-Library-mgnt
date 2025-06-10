using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using AutoMapper;
using Data.Entities;

namespace Application.Borrowings.Mappings;

public class BorrowingMapping : Profile
{
    public BorrowingMapping()
    {
        CreateMap<Borrowing, BorrowingDto>().ReverseMap();
        CreateMap<Borrowing, CreateBorrowingDto>().ReverseMap();
        CreateMap<Borrowing, UpdateBorrowingDto>().ReverseMap();
        CreateMap<UpdateBorrowingDto, UpdateBorrowingCommand>().ReverseMap();
        CreateMap<CreateBorrowingDto, CreateBorrowingCommand>().ReverseMap();
    }
}
