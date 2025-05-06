using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Helper.Pagination
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPaginationForm<T>(
        this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedResult<T>
            {
                TotalCount = totalCount,
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }

}
