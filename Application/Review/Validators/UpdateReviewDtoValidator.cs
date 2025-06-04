using Application.Review.Dtos;
using FluentValidation;

namespace Application.Review.Validators
{
    public class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewDtoValidator()
        {
            RuleFor(x => x.Rating).NotEmpty().WithMessage("you must rate this book");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("a review must have a comment");
        }
    }
}
