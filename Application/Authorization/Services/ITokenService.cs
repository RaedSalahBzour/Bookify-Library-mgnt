using Bookify_Library_mgnt.Common;
using Domain.Entities;
using System.Security.Claims;
namespace Application.Authorization.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
        Task<string> GenerateAndSaveRefreshTokenAsync(User user);
        Task<Result<User?>> ValidateRefreshTokenAsync(string userId, string RefreshToken);

    }
}
