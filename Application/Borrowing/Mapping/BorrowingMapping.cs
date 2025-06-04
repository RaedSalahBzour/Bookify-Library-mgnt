using AutoMapper;
using Bookify_Library_mgnt.Dtos.Borrowings;
using Domain.Entities;

namespace Bookify_Library_mgnt.Mappings
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
