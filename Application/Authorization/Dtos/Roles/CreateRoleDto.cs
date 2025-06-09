using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Dtos.Roles
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Role Name is required")]
        [MinLength(3)]
        public string RoleName { get; set; }
    }
}
