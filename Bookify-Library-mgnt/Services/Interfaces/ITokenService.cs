using Bookify_Library_mgnt.Models;
using System.Security.Claims;

namespace Bookify_Library_mgnt.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
