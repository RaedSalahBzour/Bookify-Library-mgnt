using Application.Authorization.Services;
using Data.Interfaces;
using Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public TokenService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var claims = await GetAllValidClaimsAsync(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_configuration.GetValue<string>("JWT:Key")!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JWT:Issuer"),
                audience: _configuration.GetValue<string>("JWT:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.AuthRepository.UpdateAsync(user);
            return refreshToken;

        }
        public async Task<User?> ValidateRefreshTokenAsync(string userId, string RefreshToken)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userId);
            if (user is null)
                throw new KeyNotFoundException($"User with Email '{userId}' was not found.");

            if (user.RefreshToken != RefreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");

            return user;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private async Task<List<Claim>> GetAllValidClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
            };
            var userClaims = await _unitOfWork.AuthRepository.GetUserClaimsAsync(user);
            claims.AddRange(userClaims);
            var userRoles = await _unitOfWork.RoleRepository.GetUserRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var role = await _unitOfWork.RoleRepository.FindRoleByNameAsync(userRole);
                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    var roleClaims = await _unitOfWork.RoleRepository.GetRoleClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }
    }
}
