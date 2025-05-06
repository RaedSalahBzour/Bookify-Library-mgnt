using Bookify_Library_mgnt.Dtos.Reviews;
using FluentValidation;

namespace Bookify_Library_mgnt.Helper.Validation.ReviewValidation
{
    public class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewDtoValidator()
        {
            RuleFor(x => x.Rating).NotEmpty().WithMessage("you must rate this book");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("a review must have a comment");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId Date Is Required");
            RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId Date Is Required");
        }
    }
}
