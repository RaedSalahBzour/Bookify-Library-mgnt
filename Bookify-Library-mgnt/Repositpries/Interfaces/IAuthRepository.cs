using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IAuthRepository
    {
        Task<PagedResult<UserDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 10);
        Task<UserDto> GetUserByIdAsync(string id);
        Task<User> CreateAsync(CreateUserDto userDto);
        Task<User> UpdateUserAsync(string id, UpdateUserDto userDto);
        Task<string> DeleteUserAsync(string id);

    }
}
