using Application.Books.Commands;
using Application.Books.Dtos;
using Application.Books.Services;
using AutoMapper;
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
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result<BookDto>>
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<Result<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var updateBookDto = _mapper.Map<UpdateBookDto>(request);
            var result = await _bookService.UpdateBookAsync(request.Id, updateBookDto);
            if (!result.IsSuccess)
            {
                return Result<BookDto>.Fail(ErrorMessages.OperationFailed(nameof(OperationNames.UpdateBook), result.Errors));
            }
            return Result<BookDto>.Ok(result.Data);
        }
    }
}
