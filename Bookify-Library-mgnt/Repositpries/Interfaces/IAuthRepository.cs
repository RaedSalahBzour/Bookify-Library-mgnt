using Bookify_Library_mgnt.Dtos.Books;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IAuthRepository
    {
        IQueryable<User> GetUsersAsync();
        Task<User> GetUserByIdAsync(string id);

    }
}
