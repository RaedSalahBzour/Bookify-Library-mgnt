using Bookify_Library_mgnt.Common;
using Domain.Entities;
using System.Security.Claims;
namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
        Task<string> GenerateAndSaveRefreshTokenAsync(User user);
        Task<Result<User?>> ValidateRefreshTokenAsync(string userId, string RefreshToken);

    }
}
