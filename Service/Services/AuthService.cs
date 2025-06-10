using Application.Authorization.Dtos.Token;
using Application.Authorization.Services;
using Application.Users.Dtos;
using Application.Users.Services;
using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Interfaces;

namespace Service.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    public AuthService(IMapper mapper,
        ITokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var users = _unitOfWork.AuthRepository.GetUsersAsync();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return usersDto;
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(id);
        if (user == null) throw new KeyNotFoundException($"User with id '{id}' was not found.");
        return _mapper.Map<UserDto>(user);
    }
    public async Task<UserDto> CreateAsync(CreateUserDto userDto)
    {
        var existingUserByEmail = await _unitOfWork.AuthRepository
            .GetUserByEmailAsync(userDto.Email);
        if (existingUserByEmail != null)
            throw new InvalidOperationException(
                $"Email '{userDto.Email}' already exists.");

        var existingUserByUsername = await _unitOfWork.AuthRepository
            .GetUserByNameAsync(userDto.UserName);
        if (existingUserByUsername != null)
            throw new InvalidOperationException(
                $"User Name '{userDto.UserName}' already exists.");

        var user = _mapper.Map<User>(userDto);
        var result = await _unitOfWork.AuthRepository.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                    $"Operation '{OperationNames.CreateUser}' failed to complete. ");
        }
        await _unitOfWork.AuthRepository.AddToRoleAsync(user, "user");
        var Dto = _mapper.Map<UserDto>(user);
        return Dto;
    }
    public async Task<UserDto> UpdateUserAsync(string id, UpdateUserDto userDto)
    {
        var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(id);
        if (user == null) throw new InvalidOperationException(
                $"User with Id '{id}' Was Not Found.");

        _mapper.Map(userDto, user);
        var result = await _unitOfWork.AuthRepository.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                                  $"Operation '{OperationNames.UpdateUser}' failed to complete.");
        }
        return _mapper.Map<UserDto>(user);
    }
    public async Task<UserDto> DeleteUserAsync(string id)
    {
        var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(id);
        if (user == null) throw new InvalidOperationException(
                $"User with Id '{id}' Was Not Found.");
        var result = await _unitOfWork.AuthRepository.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Operation '{OperationNames.DeleteUser}' failed to complete.");
        }
        return _mapper.Map<UserDto>(user);
    }
    public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
    {


        var user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(loginDto.Email);
        if (user is null)
            throw new InvalidOperationException(
                      $"Operation '{OperationNames.Login}' failed to complete. Incorrect Password Or Email");
        var isPasswordValid = _unitOfWork.AuthRepository.VerifyPasswordAsync(user, loginDto.Password);

        if (isPasswordValid == false)
            throw new InvalidOperationException(
                      $"Operation '{OperationNames.Login}' failed to complete. Incorrect Password Or Email");

        var token = await _tokenService.GenerateTokenAsync(user);
        return await CreateTokenResponse(user);
    }
    private async Task<TokenResponseDto> CreateTokenResponse(User user)
    {
        return new TokenResponseDto
        {
            AccessToken = await _tokenService.GenerateTokenAsync(user),
            RefreshToken = await _tokenService.GenerateAndSaveRefreshTokenAsync(user)
        };
    }

    public async Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto requestDto)
    {
        var user = await _tokenService
                    .ValidateRefreshTokenAsync(requestDto.UserId, requestDto.RefreshToken);

        return await CreateTokenResponse(user);


    }
}
