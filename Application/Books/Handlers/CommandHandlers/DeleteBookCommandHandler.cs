using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Handlers.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result<BookDto>>
    {
        private readonly IBookService _bookService;

        public DeleteBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Result<BookDto>> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var result = await _bookService.DeleteBookAsync(command.id);
            if (!result.IsSuccess)
                return Result<BookDto>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.DeleteBook), result.Errors));
            return Result<BookDto>.Ok(result.Data);
        }
    }
}
