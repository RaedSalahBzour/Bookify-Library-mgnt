using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using AutoMapper;
using MediatR;

namespace Application.Books.Handlers.CommandHandlers;

public class UpdateBookCommandHandler(IBookService bookService, IMapper mapper)
    : IRequestHandler<UpdateBookCommand, BookDto>
{

    public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var updateBookDto = mapper.Map<UpdateBookDto>(request);
        return await bookService.UpdateBookAsync(request.Id, updateBookDto);

    }
}
