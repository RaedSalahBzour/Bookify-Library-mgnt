using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Reviews;
using Bookify_Library_mgnt.Helper;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Repositpries.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ReviewRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReviewDto>> GetReviewsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Reviews.AsQueryable();
            var totalCount = await query.CountAsync();
            var reviews = await query.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return new PagedResult<ReviewDto>
            {
                TotalCount = totalCount,
                Items = reviewsDto,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
        public async Task<ReviewDto> GetReviewByIdAsync(string id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review is null)
            {
                return null;
            }
            var reviewDto = _mapper.Map<ReviewDto>(review);
            return reviewDto;
        }

        public async Task<Review> CreateReviewAsync(CreateReviewDto dto)
        {

            var review = _mapper.Map<Review>(dto);
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == dto.BookId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
            if (user == null && book == null) return null;
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }
        public async Task<Review> UpdateReviewAsync(string id, UpdateReviewDto dto)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review is null)
            {
                return null;
            }
            _mapper.Map(dto, review);
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return review;

        }

        public async Task<string> DeleteReviewAsync(string id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review is null)
            {
                return null;
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return review.Id;
        }

    }
}
