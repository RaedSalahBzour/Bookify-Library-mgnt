using Application.Books.Dtos;
using FluentValidation;

namespace Application.Books.Validators
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("Title must have at least 3 letters");
            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required")
                .MinimumLength(3).WithMessage("Author must have at least 3 letters");
            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required");
            RuleFor(x => x.PublishDate)
                .NotEmpty().WithMessage("PublishDate is required");
            RuleFor(x => x.CategoryIds)
                .NotEmpty().WithMessage("CategoryIds is required")
                .Must(ids => ids.All(id => Guid.TryParse(id, out _)))
                .WithMessage("All CategoryIds must be valid GUIDs.");
        }

    }
}
