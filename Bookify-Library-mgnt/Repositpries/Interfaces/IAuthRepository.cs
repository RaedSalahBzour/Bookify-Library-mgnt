using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IAuthRepository
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<User> CreateAsync(CreateUserDto userDto);
        Task<User> UpdateUserAsync(string id, UpdateUserDto userDto);
        Task<string> DeleteUserAsync(string id);

    }
}
