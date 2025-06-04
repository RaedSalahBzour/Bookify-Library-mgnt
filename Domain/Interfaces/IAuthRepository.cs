using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthRepository
    {
        IQueryable<User> GetUsersAsync();
        Task<User> GetUserByIdAsync(string id);

    }
}
