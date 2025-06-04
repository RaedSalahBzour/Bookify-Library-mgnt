using Application.Authorization.Dtos.Token;
using Application.User.Dtos;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;

namespace Application.User.Services
{
    public interface IAuthService
    {
        Task<PagedResult<UserDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<UserDto>> GetUserByIdAsync(string id);
        Task<Result<User>> CreateAsync(CreateUserDto userDto);
        Task<Result<UserDto>> UpdateUserAsync(string id, UpdateUserDto userDto);
        Task<Result<UserDto>> DeleteUserAsync(string id);
        Task<Result<TokenResponseDto?>> LoginAsync(LoginDto loginDto);
        Task<Result<TokenResponseDto?>> RefreshTokenAsync(RefreshTokenRequestDto requestDto);
    }
}
