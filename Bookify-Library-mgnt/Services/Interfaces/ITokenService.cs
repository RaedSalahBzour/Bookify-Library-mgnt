using Bookify_Library_mgnt.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
