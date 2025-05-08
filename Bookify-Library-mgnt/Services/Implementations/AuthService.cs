using AutoMapper;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Bookify_Library_mgnt.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, IAuthRepository authRepository, IMapper mapper)
        {
            _userManager = userManager;
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            var users = _authRepository.GetUsersAsync();
            var paginatedUsers = await users.ToPaginationForm(pageNumber, pageSize);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(paginatedUsers.Items);
            return new PagedResult<UserDto>
            {
                TotalCount = paginatedUsers.TotalCount,
                Items = usersDto,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _authRepository.GetUserByIdAsync(id);
            if (user == null) { return null; }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task<User> CreateAsync(CreateUserDto userDto)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(userDto.Email);
            var existingUserByUsername = await _userManager.FindByNameAsync(userDto.UserName);

            if (existingUserByEmail != null || existingUserByUsername != null)
            {
                return null;
            }
            var user = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            return result.Succeeded ? user : null;
        }
        public async Task<User> UpdateUserAsync(string id, UpdateUserDto userDto)
        {
            var user = await _authRepository.GetUserByIdAsync(id);
            if (user == null) { return null; }
            _mapper.Map(userDto, user);
            await _userManager.UpdateAsync(user);
            return user;


        }
        public async Task<string> DeleteUserAsync(string id)
        {
            var user = await _authRepository.GetUserByIdAsync(id);
            if (user == null) { return null; }
            await _userManager.DeleteAsync(user);
            return user.Id;
        }
    }
}
