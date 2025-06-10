using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using AutoMapper;
using Data.Entities;

namespace Application.Reviews.Mapping;

public class ReviewMapping : Profile
{
    public ReviewMapping()
    {
        CreateMap<Review, ReviewDto>().ReverseMap();
        CreateMap<Review, CreateReviewDto>().ReverseMap();
        CreateMap<CreateReviewCommand, CreateReviewDto>().ReverseMap();
        CreateMap<Review, UpdateReviewDto>().ReverseMap();
        CreateMap<UpdateReviewCommand, UpdateReviewDto>().ReverseMap();
    }
}
