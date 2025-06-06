using Application.Books.Dtos;
using Application.Books.Queries;
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

namespace Application.Books.Handlers.QueryHandlers
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Result<BookDto>>
    {
        private readonly IBookService _bookService;

        public GetBookByIdQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Result<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookService.GetByIdAsync(request.id);
            if (!result.IsSuccess)
                return Result<BookDto>.Fail(ErrorMessages.
                    OperationFailed(nameof(OperationNames.GetBook), result.Errors));
            return Result<BookDto>.Ok(result.Data);
        }
    }
}
