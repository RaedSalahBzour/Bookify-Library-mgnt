using Bookify_Library_mgnt.Dtos.Users;
using FluentValidation;

namespace Bookify_Library_mgnt.Validation.UserValidation
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("username is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("email is required")
                .EmailAddress().WithMessage("invalid email format");
            RuleFor(x => x.Password).NotEmpty().WithMessage("password is required")
                .MinimumLength(8).WithMessage("password must be at least 8 letters");
        }
    }
}
