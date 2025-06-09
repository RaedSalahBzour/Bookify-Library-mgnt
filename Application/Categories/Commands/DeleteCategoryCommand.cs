using Application.Categories.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands
{
    public record DeleteCategoryCommand(string Id) : IRequest<Result<CategoryDto>>
    {
        [Required(ErrorMessage = "Category ID is required")]
        public string Id { get; init; }
    }
}
