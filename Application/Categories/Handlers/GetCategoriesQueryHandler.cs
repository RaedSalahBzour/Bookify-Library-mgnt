using Application.Categories.Dtos;
using Application.Categories.Queries;
using Application.Categories.Services;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
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
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<PagedResult<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetCategoriesAsync(query.pageNumber, query.pageSize);

            return new PagedResult<CategoryDto>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                Items = result.Items,
                TotalCount = result.TotalCount
            };
        }
    }
}
