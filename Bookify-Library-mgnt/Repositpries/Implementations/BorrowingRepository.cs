using AutoMapper;
using Bookify_Library_mgnt.Data;
using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Dtos.Users;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace Bookify_Library_mgnt.Repositpries.Implementations
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BorrowingRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PagedResult<BorrowingDto>> GetBorrowingsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Borrowings.AsQueryable();
            var totalBorrowings = await query.CountAsync();
            var borrowings = await query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
            var borrowingsDto = _mapper.Map<IEnumerable<BorrowingDto>>(borrowings);
            return new PagedResult<BorrowingDto>
            {
                Items = borrowingsDto,
                TotalCount = totalBorrowings,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<BorrowingDto> GetBorrowingByIdAsync(string id)
        {
            var borrowing = await _context.Borrowings.FirstOrDefaultAsync(x => x.Id == id);
            if (borrowing == null) { return null; }
            var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
            return borrowingDto;
        }
        public async Task<Borrowing> CreateBorrowingAsync(CreateBorrowingDto borrowingDto)
        {
            var borrowing = _mapper.Map<Borrowing>(borrowingDto);
            await _context.Borrowings.AddAsync(borrowing);
            await _context.SaveChangesAsync();
            return borrowing;
        }

        public async Task<Borrowing> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowingdto)
        {
            var borrowing = await _context.Borrowings.FirstOrDefaultAsync(x => x.Id == id);
            if (borrowing == null) { return null; }
            _mapper.Map(borrowingdto, borrowing);
            _context.Borrowings.Update(borrowing);
            await _context.SaveChangesAsync();
            return borrowing;
        }
        public async Task<string> DeleteBorrowingAsync(string id)
        {
            var borrowing = await _context.Borrowings.FirstOrDefaultAsync(x => x.Id == id);
            if (borrowing == null) { return null; }
            _context.Borrowings.Remove(borrowing);
            await _context.SaveChangesAsync();
            return borrowing.Id;
        }

    }


    public static class BorrowingEndpoints
    {
        public static void MapBorrowingEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Borrowing").WithTags(nameof(Borrowing));

            group.MapGet("/", () =>
            {
                return new[] { new Borrowing() };
            })
            .WithName("GetAllBorrowings")
            .WithOpenApi();

            group.MapGet("/{id}", (int id) =>
            {
                //return new Borrowing { ID = id };
            })
            .WithName("GetBorrowingById")
            .WithOpenApi();

            group.MapPut("/{id}", (int id, Borrowing input) =>
            {
                return TypedResults.NoContent();
            })
            .WithName("UpdateBorrowing")
            .WithOpenApi();

            group.MapPost("/", (Borrowing model) =>
            {
                //return TypedResults.Created($"/api/Borrowings/{model.ID}", model);
            })
            .WithName("CreateBorrowing")
            .WithOpenApi();

            group.MapDelete("/{id}", (int id) =>
            {
                //return TypedResults.Ok(new Borrowing { ID = id });
            })
            .WithName("DeleteBorrowing")
            .WithOpenApi();
        }
    }
}
