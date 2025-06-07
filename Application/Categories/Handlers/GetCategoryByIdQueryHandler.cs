using Application.Categories.Dtos;
using Application.Categories.Queries;
using Application.Categories.Services;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Categories.Handlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryByIdQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByIdAsync(query.id);
            if (!result.IsSuccess)
                return Result<CategoryDto>.Fail(ErrorMessages
                    .OperationFailed(nameof(OperationNames.GetCategoryById), result.Errors));
            return Result<CategoryDto>.Ok(result.Data);
        }
    }
}
