using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;

namespace Application.Books.Handlers.CommandHandlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<BookDto>>
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<Result<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var createBookDto = _mapper.Map<CreateBookDto>(request);
            var result = await _bookService.CreateBookAsync(createBookDto);
            if (!result.IsSuccess)
                return Result<BookDto>.Fail
                    (ErrorMessages.OperationFailed(nameof(OperationNames.CreateBook),
                    result.Errors));
            return Result<BookDto>.Ok(result.Data);
        }
    }
}
