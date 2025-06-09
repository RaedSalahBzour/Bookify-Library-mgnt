using Application.Authorization.Dtos.Token;
using Application.Users.Dtos;

namespace Application.Users.Services
{
    public interface IAuthService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<UserDto> CreateAsync(CreateUserDto userDto);
        Task<UserDto> UpdateUserAsync(string id, UpdateUserDto userDto);
        Task<UserDto> DeleteUserAsync(string id);
        Task<TokenResponseDto> LoginAsync(LoginDto loginDto);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto requestDto);
    }
}
