using Application.Books.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Books.Commands
{
    public class UpdateBookCommand : IRequest<Result<BookDto>>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public string Isbn { get; set; }
        public DateTime PublishDate { get; set; }
        public List<string> CategoryIds { get; set; }

        [JsonIgnore]
        public string? Id { get; set; }
    }

}
