using Domain.Entities;

namespace Bookify_Library_mgnt.Repositpries.Interfaces
{
    public interface IAuthRepository
    {
        IQueryable<User> GetUsersAsync();
        Task<User> GetUserByIdAsync(string id);

    }
}
