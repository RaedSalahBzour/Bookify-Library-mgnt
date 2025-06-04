using Application.Authorization.Dtos.Roles;
using FluentValidation;

namespace Application.Authorization.Validators
{
    public class UpdateRoleDtoValidator : AbstractValidator<UpdateRoleDto>
    {
        public UpdateRoleDtoValidator()
        {
            RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Role name is required.")
            .MinimumLength(3).WithMessage("Role name must be at least 3 characters.")
            .MaximumLength(30).WithMessage("Role name must be less than 30 characters.");
        }
    }
}
