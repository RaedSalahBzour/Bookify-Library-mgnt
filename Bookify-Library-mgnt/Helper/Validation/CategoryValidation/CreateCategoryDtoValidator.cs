using Bookify_Library_mgnt.Dtos.Categories;
using FluentValidation;

namespace Bookify_Library_mgnt.Helper.Validation.CategoryValidation
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
