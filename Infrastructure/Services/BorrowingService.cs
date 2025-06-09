using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using Data.Interfaces;
using AutoMapper;
using Data.Entities;
namespace Infrastructure.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BorrowingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BorrowingDto>> GetBorrowingsAsync()
        {
            var borrowings = _unitOfWork.BorrowingRepository.GetAll();
            return _mapper.Map<List<BorrowingDto>>(borrowings);

        }
        public async Task<BorrowingDto> GetBorrowingByIdAsync(string id)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
            if (borrowing == null)
                throw new KeyNotFoundException($"Borrowing With Id {id} Was Not Found");
            return _mapper.Map<BorrowingDto>(borrowing);
        }

        public async Task<BorrowingDto> CreateBorrowingAsync(CreateBorrowingDto borrowingDto)
        {
            var borrowing = _mapper.Map<Borrowing>(borrowingDto);
            var bookExist = await _unitOfWork.BookRepository.GetByIdAsync(borrowingDto.BookId);
            var userExist = await _unitOfWork.AuthRepository.GetUserByIdAsync(borrowingDto.UserId);
            if (bookExist is null)

                throw new KeyNotFoundException($"Book With Id {borrowingDto.BookId} Was Not Found");

            if (userExist is null)
                throw new KeyNotFoundException($"User With Id {borrowingDto.UserId} Was Not Found");

            await _unitOfWork.BorrowingRepository.AddAsync(borrowing);
            await _unitOfWork.BorrowingRepository.SaveChangesAsync();
            return _mapper.Map<BorrowingDto>(borrowing);

        }
        public async Task<BorrowingDto> UpdateBorrowingAsync(string id, UpdateBorrowingDto borrowingDto)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
            if (borrowing == null)
                throw new KeyNotFoundException($"Borrowing With Id {id} Was Not Found");

            _mapper.Map(borrowingDto, borrowing);
            var (userExists, bookExists) =
                await _unitOfWork.BorrowingRepository
                .CheckUserAndBookExistAsync(borrowingDto.UserId, borrowingDto.BookId);


            if (!userExists)
                throw new KeyNotFoundException($"User with ID '{borrowingDto.UserId}' not found.");

            if (!bookExists)
                throw new KeyNotFoundException($"Book with ID '{borrowingDto.BookId}' not found.");

            await _unitOfWork.BorrowingRepository.Update(borrowing);
            await _unitOfWork.BorrowingRepository.SaveChangesAsync();
            return _mapper.Map<BorrowingDto>(borrowing);
        }

        public async Task<BorrowingDto> DeleteBorrowingAsync(string id)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
            if (borrowing == null)
                throw new KeyNotFoundException($"Borrowing With Id {id} Was Not Found");

            await _unitOfWork.BorrowingRepository.Delete(borrowing);
            await _unitOfWork.BorrowingRepository.SaveChangesAsync();
            return _mapper.Map<BorrowingDto>(borrowing);

        }



    }
}