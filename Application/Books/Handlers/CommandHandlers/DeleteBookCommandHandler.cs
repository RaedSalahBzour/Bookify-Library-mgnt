using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using MediatR;

namespace Application.Books.Handlers.CommandHandlers;

public class DeleteBookCommandHandler(IBookService bookService)
    : IRequestHandler<DeleteBookCommand, BookDto>
{

    public async Task<BookDto> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        return await bookService.DeleteBookAsync(command.Id);

    }
}
