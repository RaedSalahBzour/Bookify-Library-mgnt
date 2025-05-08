using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface IAuthService
    {
        Task<PagedResult<UserDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<UserDto>> GetUserByIdAsync(string id);
        Task<Result<User>> CreateAsync(CreateUserDto userDto);
        Task<Result<UserDto>> UpdateUserAsync(string id, UpdateUserDto userDto);
        Task<Result<UserDto>> DeleteUserAsync(string id);
    }
}
