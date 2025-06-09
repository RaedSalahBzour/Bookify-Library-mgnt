using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using AutoMapper;
using MediatR;

namespace Application.Books.Handlers.CommandHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var updateBookDto = _mapper.Map<UpdateBookDto>(request);
            return await _bookService.UpdateBookAsync(request.Id, updateBookDto);

        }
    }
}
