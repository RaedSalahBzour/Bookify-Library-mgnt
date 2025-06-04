using AutoMapper;
using Bookify_Library_mgnt.Dtos.Reviews;
using Domain.Entities;

namespace Bookify_Library_mgnt.Mappings
{
    public class ReviewMapping : Profile
    {
        public ReviewMapping()
        {
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
        }
    }
}
