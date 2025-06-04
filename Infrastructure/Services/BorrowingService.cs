using Application.Borrowing.Dtos;
using Application.Borrowing.Services;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;
namespace Infrastructure.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBorrowingDto> _crateValidator;
        private readonly IValidator<UpdateBorrowingDto> _updateValidator;
        public BorrowingService(IBorrowingRepository borrowingRepository, IMapper mapper,
            IValidator<CreateBorrowingDto> crateValidator,
            IValidator<UpdateBorrowingDto> updateValidator)
        {
            _borrowingRepository = borrowingRepository;
            _mapper = mapper;
            _crateValidator = crateValidator;
            _updateValidator = updateValidator;
        }

        public async Task<PagedResult<BorrowingDto>> GetBorrowingsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var borrowings = _borrowingRepository.GetBorrowingsAsync();
            var paginatedBorrowings = await borrowings.ToPaginationForm(pageNumber, pageSize);
            var borrowingsDto = _mapper.Map<IEnumerable<BorrowingDto>>(paginatedBorrowings.Items);
            return new PagedResult<BorrowingDto>
            {
                TotalCount = paginatedBorrowings.TotalCount,
                Items = borrowingsDto,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<Result<BorrowingDto>> GetBorrowingByIdAsync(string id)
        {
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
            if (borrowing == null) { return Result<BorrowingDto>.Fail(ErrorMessages.NotFoundById(id)); }
            var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
            return Result<BorrowingDto>.Ok(borrowingDto);
        }

        public async Task<Result<Borrowing>> CreateBorrowingAsync(CreateBorrowingDto borrowingDto)
        {
            var validationResult = await _crateValidator.ValidateAsync(borrowingDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Borrowing>.Fail(errorMessages);
            }
            var borrowing = _mapper.Map<Borrowing>(borrowingDto);
            var (userExists, bookExists) = await _borrowingRepository.
                CheckUserAndBookExistAsync(borrowingDto.UserId, borrowingDto.BookId);
            if (!userExists || !bookExists)
            {
                var errors = new List<string>();

                if (!userExists)
                    errors.Add($"User with ID {borrowingDto.UserId} not found");

                if (!bookExists)
                    errors.Add($"Book with ID {borrowingDto.BookId} not found");

                return Result<Borrowing>.Fail(string.Join(" | ", errors));
            }
            await _borrowingRepository.CreateBorrowingAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            return Result<Borrowing>.Ok(borrowing);
        }
        public async Task<Result<Borrowing>> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowingDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(borrowingDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Borrowing>.Fail(errorMessages);
            }
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
            if (borrowing == null) { return Result<Borrowing>.Fail(ErrorMessages.NotFoundById(id)); }
            _mapper.Map(borrowingDto, borrowing);
            await _borrowingRepository.UpdateBorrowingAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            return Result<Borrowing>.Ok(borrowing);
        }

        public async Task<Result<Borrowing>> DeleteBorrowingAsync(string id)
        {
            var borrowing = await _borrowingRepository.GetBorrowingByIdAsync(id);
            if (borrowing == null) { return Result<Borrowing>.Fail(ErrorMessages.NotFoundById(id)); }
            await _borrowingRepository.DeleteBorrowingAsync(borrowing);
            await _borrowingRepository.SaveChangesAsync();
            return Result<Borrowing>.Ok(borrowing);
        }



    }
}
