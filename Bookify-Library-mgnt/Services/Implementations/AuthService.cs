using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Bookify_Library_mgnt.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserDto> _createValidator;
        private readonly IValidator<UpdateUserDto> _updateValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<User> userManager,
            IAuthRepository authRepository, IMapper mapper,
            IValidator<CreateUserDto> createValidator,
            IValidator<UpdateUserDto> updateValidator,
            IValidator<LoginDto> loginValidator, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _authRepository = authRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _loginValidator = loginValidator;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

        public async Task<Result<UserDto>> GetUserByIdAsync(string id)
        {
            var user = await _authRepository.GetUserByIdAsync(id);
            if (user == null) { return Result<UserDto>.Fail(ErrorMessages.NotFound(id)); }
            return Result<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }
        public async Task<Result<User>> CreateAsync(CreateUserDto userDto)
        {
            var validationResult = await _createValidator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<User>.Fail(errorMessages);
            }
            var existingUserByEmail = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUserByEmail != null)
                return Result<User>.Fail(ErrorMessages.EmailAlreadyExists(userDto.Email));

            var existingUserByUsername = await _userManager.FindByNameAsync(userDto.UserName);
            if (existingUserByUsername != null)
                return Result<User>.Fail(ErrorMessages.UsernameAlreadyExists(userDto.UserName));

            var user = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                return Result<User>.Fail(ErrorMessages.OperationFailed("Create", null));
            }
            await _userManager.AddToRoleAsync(user, "user");
            return Result<User>.Ok(user);
        }
        public async Task<Result<UserDto>> UpdateUserAsync(string id, UpdateUserDto userDto)
        {
            var user = await _authRepository.GetUserByIdAsync(id);
            if (user == null) return Result<UserDto>.Fail(ErrorMessages.NotFound(id));
            var validationResult = _updateValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<UserDto>.Fail(errorMessages);
            }
            _mapper.Map(userDto, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Result<UserDto>.Fail(ErrorMessages.OperationFailed("Update", new List<string>()));
            }
            return Result<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }
        public async Task<Result<UserDto>> DeleteUserAsync(string id)
        {
            var user = await _authRepository.GetUserByIdAsync(id);
            if (user == null) return Result<UserDto>.Fail(ErrorMessages.NotFound(id));
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return Result<UserDto>.Fail(ErrorMessages.OperationFailed("Delete", new List<string>()));
            }
            return Result<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }
        public async Task<Result<string?>> LoginAsync(LoginDto loginDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<string?>.Fail(errorMessages);
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                return Result<string?>.Fail(ErrorMessages.LoginFail());
            }
            var userHashPassword = new PasswordHasher<User>().HashPassword(user, loginDto.Password);
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, loginDto.Password)
                == PasswordVerificationResult.Failed)
            {
                return Result<string?>.Fail(ErrorMessages.LoginFail());
            }

            var token = await _tokenService.GenerateToken(user);

            return Result<string?>.Ok(token);

        }
    }
}
