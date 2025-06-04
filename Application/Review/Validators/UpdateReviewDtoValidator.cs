using Bookify_Library_mgnt.Dtos.Reviews;
using FluentValidation;

namespace Bookify_Library_mgnt.Validation.ReviewValidation
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
