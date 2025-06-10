using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using AutoMapper;
using MediatR;

namespace Application.Books.Handlers.CommandHandlers;

public class CreateBookCommandHandler(IBookService bookService, IMapper mapper)
    : IRequestHandler<CreateBookCommand, BookDto>
{
    public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var createBookDto = mapper.Map<CreateBookDto>(request);
        return await bookService.CreateBookAsync(createBookDto);

    }
}
