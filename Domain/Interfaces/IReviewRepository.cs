using Domain.Entities;


namespace Domain.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<(bool userExists, bool bookExists)> CheckUserAndBookExistAsync(string userId, string bookId);

    }
}
