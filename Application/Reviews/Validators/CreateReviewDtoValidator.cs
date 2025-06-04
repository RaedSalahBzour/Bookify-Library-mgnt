using Application.Reviews.Dtos;
using FluentValidation;

namespace Application.Reviews.Validators
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
