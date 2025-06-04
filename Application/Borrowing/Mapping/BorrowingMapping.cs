using Application.Borrowing.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Borrowing.Mappings
{
    public class BorrowingMapping : Profile
    {
        public BorrowingMapping()
        {
            CreateMap<Borrowing, BorrowingDto>().ReverseMap();
            CreateMap<Borrowing, CreateBorrowingDto>().ReverseMap();
            CreateMap<Borrowing, UpdateBorrowingDto>().ReverseMap();
        }
    }
}
