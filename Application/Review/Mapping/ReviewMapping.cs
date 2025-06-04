using Application.Review.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Review.Mapping
{
    public class ReviewMapping : Profile
    {
        public ReviewMapping()
        {
            CreateMap<Review, ReviewDSto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
        }
    }
}
