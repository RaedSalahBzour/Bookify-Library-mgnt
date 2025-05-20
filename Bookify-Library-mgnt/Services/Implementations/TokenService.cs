using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bookify_Library_mgnt.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public TokenService(IConfiguration configuration, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
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
            var result = await _userManager.UpdateAsync(user);
            return refreshToken;

        }
        public async Task<Result<User?>> ValidateRefreshTokenAsync(string userId, string RefreshToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<User?>.Fail(ErrorMessages.NotFoundById(userId));
            }
            if (user.RefreshToken != RefreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return Result<User?>.Fail(ErrorMessages.InvalidRefreshToken());
            }

            return Result<User?>.Ok(user);
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
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
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
