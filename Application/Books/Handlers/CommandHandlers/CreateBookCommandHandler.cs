using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using AutoMapper;
using MediatR;

namespace Application.Books.Handlers.CommandHandlers;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public CreateBookCommandHandler(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var createBookDto = _mapper.Map<CreateBookDto>(request);
        return await _bookService.CreateBookAsync(createBookDto);

    }
}
