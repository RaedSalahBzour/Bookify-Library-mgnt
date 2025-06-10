using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using MediatR;

namespace Application.Books.Handlers.CommandHandlers;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, BookDto>
{
    private readonly IBookService _bookService;

    public DeleteBookCommandHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<BookDto> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        return await _bookService.DeleteBookAsync(command.Id);

    }
}
