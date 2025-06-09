using Application.Books.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands
{
    public record DeleteBookCommand(string Id) : IRequest<Result<BookDto>>
    {
        [Required(ErrorMessage = "Book Id is required")]
        public string Id { get; init; }
    }
}
