using Application.Borrowings.Commands;
using Application.Borrowings.Dtos;
using Application.Borrowings.Services;
using AutoMapper;
using MediatR;

namespace Application.Borrowings.Handlers
{
    internal class UpdateBorrowingCommandHanlder : IRequestHandler<UpdateBorrowingCommand, BorrowingDto>
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IMapper _mapper;

        public UpdateBorrowingCommandHanlder(IBorrowingService borrowingService, IMapper mapper)
        {
            _borrowingService = borrowingService;
            _mapper = mapper;
        }
        public async Task<BorrowingDto> Handle(UpdateBorrowingCommand command, CancellationToken cancellationToken)
        {
            var updateBorrowingDto = _mapper.Map<UpdateBorrowingDto>(command);
            return await _borrowingService.UpdateBorrowingAsync(command.Id, updateBorrowingDto);

        }
    }
}
