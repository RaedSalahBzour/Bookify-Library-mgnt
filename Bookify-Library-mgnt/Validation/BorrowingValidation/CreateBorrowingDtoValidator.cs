using Bookify_Library_mgnt.Dtos.Borrowings;
using FluentValidation;

namespace Bookify_Library_mgnt.Validation.BorrowingValidation
{
    public class CreateBorrowingDtoValidator : AbstractValidator<CreateBorrowingDto>
    {
        public CreateBorrowingDtoValidator()
        {
            RuleFor(x => x.BorrowedOn)
                 .NotEmpty()
                 .WithMessage("Borrowing Date Is Required")
                 .GreaterThan(DateTime.Now)
                 .WithMessage("Borrowing date must be in the future.");
            RuleFor(x => x.ReturnedOn)
                .NotEmpty()
                .WithMessage("Returning Date Is Required")
                .GreaterThan(x => x.BorrowedOn)
                .WithMessage("Returning date must be after Borrowing date."); ;
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId Date Is Required");
            RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId Date Is Required");
        }

    }
}
