using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Repositpries.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthRepository(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Users
                .Include(u => u.Borrowings)
                .Include(u => u.Reviews)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return new PagedResult<UserDto>
            {
                Items = usersDto,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.Include(u => u.Borrowings).Include(u => u.Reviews).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            _mapper.Map(userDto, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return null;
            }
            return user;

        }
        public async Task<string> DeleteUserAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            await _userManager.DeleteAsync(user);
            return id;
        }
    }
}
