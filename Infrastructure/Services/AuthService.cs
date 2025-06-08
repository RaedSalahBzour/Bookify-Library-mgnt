using Application.Authorization.Dtos.Token;
using Application.Authorization.Services;
using Application.Common.Interfaces;
using Application.Users.Dtos;
using Application.Users.Services;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserDto> _createValidator;
        private readonly IValidator<UpdateUserDto> _updateValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly ITokenService _tokenService;
        public AuthService(IMapper mapper,
            IValidator<CreateUserDto> createValidator,
            IValidator<UpdateUserDto> updateValidator,
            IValidator<LoginDto> loginValidator, ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _loginValidator = loginValidator;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<UserDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 10)
        {

            var users = _unitOfWork.AuthRepository.GetUsersAsync();
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
            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(id);
            if (user == null) { return Result<UserDto>.Fail(ErrorMessages.NotFoundById(id)); }
            return Result<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }
        public async Task<Result<UserDto>> CreateAsync(CreateUserDto userDto)
        {
            var validationResult = await _createValidator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<UserDto>.Fail(errorMessages);
            }
            var existingUserByEmail = await _unitOfWork.AuthRepository.GetUserByEmailAsync(userDto.Email);
            if (existingUserByEmail != null)
                return Result<UserDto>.Fail(ErrorMessages.EmailAlreadyExists(userDto.Email));

            var existingUserByUsername = await _unitOfWork.AuthRepository.GetUserByNameAsync(userDto.UserName);
            if (existingUserByUsername != null)
                return Result<UserDto>.Fail(ErrorMessages.UsernameAlreadyExists(userDto.UserName));

            var user = _mapper.Map<User>(userDto);
            var result = await _unitOfWork.AuthRepository.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                return Result<UserDto>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.CreateUser), null));
            }
            await _unitOfWork.AuthRepository.AddToRoleAsync(user, "user");
            var uDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Ok(uDto);
        }
        public async Task<Result<UserDto>> UpdateUserAsync(string id, UpdateUserDto userDto)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(id);
            if (user == null) return Result<UserDto>.Fail(ErrorMessages.NotFoundById(id));
            var validationResult = _updateValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<UserDto>.Fail(errorMessages);
            }
            _mapper.Map(userDto, user);
            var result = await _unitOfWork.AuthRepository.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Result<UserDto>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.UpdateUser), new List<string>()));
            }
            return Result<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }
        public async Task<Result<UserDto>> DeleteUserAsync(string id)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(id);
            if (user == null) return Result<UserDto>.Fail(ErrorMessages.NotFoundById(id));
            var result = await _unitOfWork.AuthRepository.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return Result<UserDto>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.DeleteUser), new List<string>()));
            }
            return Result<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }
        public async Task<Result<TokenResponseDto?>> LoginAsync(LoginDto loginDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<TokenResponseDto?>.Fail(errorMessages);
            }
            var user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(loginDto.Email);
            if (user is null)
            {
                return Result<TokenResponseDto?>.Fail(ErrorMessages.LoginFail());
            }
            var isPasswordValid = _unitOfWork.AuthRepository.VerifyPasswordAsync(user, loginDto.Password);

            if (isPasswordValid == false)
            {
                return Result<TokenResponseDto?>.Fail(ErrorMessages.LoginFail());
            }

            var token = await _tokenService.GenerateTokenAsync(user);
            return Result<TokenResponseDto?>.Ok(await CreateTokenResponse(user));
        }
        private async Task<TokenResponseDto> CreateTokenResponse(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = await _tokenService.GenerateTokenAsync(user),
                RefreshToken = await _tokenService.GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        public async Task<Result<TokenResponseDto?>> RefreshTokenAsync(RefreshTokenRequestDto requestDto)
        {
            var user = await _tokenService
                        .ValidateRefreshTokenAsync(requestDto.UserId, requestDto.RefreshToken);
            if (user.Data is null)
            {
                return Result<TokenResponseDto?>.Fail(user.Errors);
            }
            var token = await CreateTokenResponse(user.Data);

            return Result<TokenResponseDto?>.Ok(token);
        }
    }
}
