using Application.Authorization.Services;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service.Services;

public class TokenService(IConfiguration configuration, IUnitOfWork unitOfWork) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<string> GenerateTokenAsync(User user)
    {
        List<Claim> claims = await GetAllValidClaimsAsync(user);
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(_configuration.GetValue<string>("JWT:Key")!));

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JWT:Issuer"),
            audience: _configuration.GetValue<string>("JWT:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials
            );

        string token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return token;
    }
    public async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        string refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _unitOfWork.AuthRepository.UpdateAsync(user);
        return refreshToken;

    }
    public async Task<User?> ValidateRefreshTokenAsync(string userId, string RefreshToken)
    {
        User? user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
        if (user is null)
            throw ExceptionManager
                .ReturnNotFound("User Not Found", $"User with Email '{userId}' was not found.");

        if (user.RefreshToken != RefreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            throw ExceptionManager
                .ReturnUnauthorized("Invalid or expired refresh token.",
                "The refresh token provided is either invalid or has expired.");

        return user;
    }
    private string GenerateRefreshToken()
    {
        byte[] randomNumber = new Byte[32];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        string refreshToken = Convert.ToBase64String(randomNumber);
        return refreshToken;
    }
    private async Task<List<Claim>> GetAllValidClaimsAsync(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email),
        };
        IList<Claim> userClaims = await _unitOfWork.AuthRepository.GetUserClaimsAsync(user);
        claims.AddRange(userClaims);
        IList<string> userRoles = await _unitOfWork.RoleRepository.GetUserRolesAsync(user);

        foreach (var userRole in userRoles)
        {
            IdentityRole role = await _unitOfWork.RoleRepository.FindRoleByNameAsync(userRole);
            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                IList<Claim> roleClaims = await _unitOfWork.RoleRepository.GetRoleClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }
        }
        return claims;
    }
}
