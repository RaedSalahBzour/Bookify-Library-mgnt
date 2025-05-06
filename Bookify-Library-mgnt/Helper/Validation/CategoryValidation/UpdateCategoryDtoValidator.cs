using Bookify_Library_mgnt.Dtos.Categories;
using FluentValidation;

namespace Bookify_Library_mgnt.Helper.Validation.CategoryValidation
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
