using Domain.Entities;
namespace Application.Authorization.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
        Task<string> GenerateAndSaveRefreshTokenAsync(User user);
        Task<User?> ValidateRefreshTokenAsync(string userId, string RefreshToken);

    }
}
