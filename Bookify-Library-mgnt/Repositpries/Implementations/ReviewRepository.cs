using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Reviews;
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

        public async Task<IEnumerable<ReviewDto>> GetReviewsAsync()
        {
            var reviews = await _context.Reviews.ToListAsync();
            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return reviewsDto;
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
