using Application.Authorization.Dtos.Token;
using Application.Authorization.Services;
using Application.Users.Dtos;
using Application.Users.Services;
using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Service.Exceptions;

namespace Service.Services;

public class AuthService(
        IMapper mapper,
        ITokenService tokenService,
        IUnitOfWork unitOfWork) : IAuthService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<List<UserDto>> GetUsersAsync()
    {
        List<User> users = _unitOfWork.AuthRepository.GetUsersAsync();
        List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
        return userDtos;
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        User? user = await _unitOfWork.AuthRepository.GetUserByIdAsync(id);
        if (user == null)
            throw ExceptionManager
                .ReturnNotFound("user Not Found", $"user with id {id} was not found");
        UserDto userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
    public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
    {
        User? existingUserByEmail = await _unitOfWork.AuthRepository
            .GetUserByEmailAsync(createUserDto.Email);
        if (existingUserByEmail != null)
            throw ExceptionManager.ReturnConflict(
                $"Email '{createUserDto.Email}' already exists.",
                "Each email address must be unique. Try a different one."
             );


        User? existingUserByUsername = await _unitOfWork.AuthRepository
            .GetUserByNameAsync(createUserDto.UserName);
        if (existingUserByUsername != null)
            throw ExceptionManager.ReturnConflict(
                 $"User name '{createUserDto.UserName}' already exists.",
                 "Each User Name must be unique. Try a different one."
                );


        User? user = _mapper.Map<User>(createUserDto);
        IdentityResult identityResult = await _unitOfWork.AuthRepository.CreateAsync(user, createUserDto.Password);
        if (!identityResult.Succeeded)
            throw ExceptionManager.ReturnInternalServerError(
         $"'{OperationNames.CreateUser}' failed to complete.",
         $"Something went wrong while trying to '{OperationNames.UpdateUser}'. Please try again later."
            );
        await _unitOfWork.AuthRepository.AddToRoleAsync(user, "user");
        UserDto? userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
    public async Task<UserDto> UpdateUserAsync(string id, UpdateUserDto UpdateUserDto)
    {
        var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(id);
        if (user == null)
            throw ExceptionManager
                .ReturnNotFound("user Not Found", $"user with id {id} was not found");

        _mapper.Map(UpdateUserDto, user);
        var result = await _unitOfWork.AuthRepository.UpdateAsync(user);
        if (!result.Succeeded)
            throw ExceptionManager.ReturnInternalServerError(
        $"'{OperationNames.UpdateUser}' failed to complete.",
        $"An unexpected error occurred while trying to '{OperationNames.UpdateUser}'. Please try again later."
    );
        UserDto uesrDto = _mapper.Map<UserDto>(user);
        return uesrDto;
    }
    public async Task<UserDto> DeleteUserAsync(string id)
    {
        User? user = await _unitOfWork.AuthRepository.GetUserByIdAsync(id);
        if (user == null)
            throw ExceptionManager.ReturnNotFound("user Not Found", $"user with id {id} was not found");
        var result = await _unitOfWork.AuthRepository.DeleteAsync(user);
        if (!result.Succeeded)
            throw ExceptionManager.ReturnInternalServerError(
            $"'{OperationNames.DeleteUser}' failed to complete.",
            $"An unexpected error occurred while trying to'{OperationNames.DeleteUser}'. Please try again later."
        );
        UserDto userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
    public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
    {


        User? user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(loginDto.Email);
        if (user is null)
            throw ExceptionManager.ReturnUnauthorized(
                $"'{OperationNames.Login}' failed",
                "Incorrect email or password"
            ); bool isPasswordValid = _unitOfWork.AuthRepository.VerifyPasswordAsync(user, loginDto.Password);

        if (isPasswordValid == false)
            throw ExceptionManager.ReturnUnauthorized(
              $"'{OperationNames.Login}' failed",
              "Incorrect email or password"
              );

        string token = await _tokenService.GenerateTokenAsync(user);
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
        User? user = await _tokenService
                    .ValidateRefreshTokenAsync(requestDto.UserId, requestDto.RefreshToken);
        if (user is null)
            throw ExceptionManager.ReturnUnauthorized("Invalid refresh token", "The refresh token is invalid or expired.");

        return await CreateTokenResponse(user);


    }
}
