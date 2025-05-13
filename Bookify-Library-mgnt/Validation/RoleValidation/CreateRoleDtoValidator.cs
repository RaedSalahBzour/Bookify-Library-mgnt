using Bookify_Library_mgnt.Dtos.Roles;
using FluentValidation;

namespace Bookify_Library_mgnt.Validation.RoleValidation
{
    public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleDtoValidator()
        {
            RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Role name is required.")
            .MinimumLength(3).WithMessage("Role name must be at least 3 characters.")
            .MaximumLength(30).WithMessage("Role name must be less than 30 characters.");
        }
    }
}
