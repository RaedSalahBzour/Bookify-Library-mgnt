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
    public record CreateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        [Required(ErrorMessage = "Category Name is required")]
        public string Name { get; init; }

        public string? Description { get; init; }
    }


}
