using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using AutoMapper;
using Data.Entities;
using Data.Interfaces;
namespace Service.Services;

public class BorrowingService(IMapper mapper, IUnitOfWork unitOfWork) : IBorrowingService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<BorrowingDto>> GetBorrowingsAsync()
    {
        List<Borrowing> borrowings = _unitOfWork.BorrowingRepository.GetAll();
        List<BorrowingDto> borrowingDtos = _mapper.Map<List<BorrowingDto>>(borrowings);
        return borrowingDtos;
    }
    public async Task<BorrowingDto> GetBorrowingByIdAsync(string id)
    {
        Borrowing? borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
        if (borrowing == null)
            throw new KeyNotFoundException($"Borrowing With Id {id} Was Not Found");
        BorrowingDto borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
        return borrowingDto;
    }

    public async Task<BorrowingDto> CreateBorrowingAsync(CreateBorrowingDto CreateBorrowingDto)
    {
        Borrowing? borrowing = _mapper.Map<Borrowing>(CreateBorrowingDto);
        Book? bookExist = await _unitOfWork.BookRepository.GetByIdAsync(CreateBorrowingDto.BookId);
        User? userExist = await _unitOfWork.AuthRepository.GetUserByIdAsync(CreateBorrowingDto.UserId);
        if (bookExist is null)

            throw new KeyNotFoundException($"Book With Id {CreateBorrowingDto.BookId} Was Not Found");

        if (userExist is null)
            throw new KeyNotFoundException($"User With Id {CreateBorrowingDto.UserId} Was Not Found");

        await _unitOfWork.BorrowingRepository.AddAsync(borrowing);
        await _unitOfWork.BorrowingRepository.SaveChangesAsync();
        BorrowingDto borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
        return borrowingDto;
    }
    public async Task<BorrowingDto> UpdateBorrowingAsync(string id, UpdateBorrowingDto UpdateBorrowingDto)
    {
        Borrowing? borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
        if (borrowing == null)
            throw new KeyNotFoundException($"Borrowing With Id {id} Was Not Found");

        _mapper.Map(UpdateBorrowingDto, borrowing);
        var (userExists, bookExists) =
            await _unitOfWork.BorrowingRepository
            .CheckUserAndBookExistAsync(UpdateBorrowingDto.UserId, UpdateBorrowingDto.BookId);


        if (!userExists)
            throw new KeyNotFoundException($"User with ID '{UpdateBorrowingDto.UserId}' not found.");

        if (!bookExists)
            throw new KeyNotFoundException($"Book with ID '{UpdateBorrowingDto.BookId}' not found.");

        await _unitOfWork.BorrowingRepository.Update(borrowing);
        await _unitOfWork.BorrowingRepository.SaveChangesAsync();
        BorrowingDto borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
        return borrowingDto;
    }

    public async Task<BorrowingDto> DeleteBorrowingAsync(string id)
    {
        Borrowing? borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
        if (borrowing == null)
            throw new KeyNotFoundException($"Borrowing With Id {id} Was Not Found");

        await _unitOfWork.BorrowingRepository.Delete(borrowing);
        await _unitOfWork.BorrowingRepository.SaveChangesAsync();
        BorrowingDto borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
        return borrowingDto;

    }



}