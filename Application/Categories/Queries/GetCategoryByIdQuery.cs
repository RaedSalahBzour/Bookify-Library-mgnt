using Application.Categories.Dtos;
using Bookify_Library_mgnt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Queries
{
    public record GetCategoryByIdQuery(string id) : IRequest<Result<CategoryDto>>;
}
