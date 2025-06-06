using Application.Books.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands
{
    public record DeleteBookCommand(string id) : IRequest<Result<BookDto>>;

}
