using Application.Users.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<UserDto>
    {
        [Required(ErrorMessage = "User ID is required")]
        public string Id { get; init; }
    }
}
