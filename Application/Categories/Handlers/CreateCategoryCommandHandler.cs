using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Categories.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateCategoryDto>(command);
            var result = await _categoryService.CreateCategoryAsync(dto);
            if (!result.IsSuccess)
                return Result<CategoryDto>.Fail(ErrorMessages
                    .OperationFailed(nameof(OperationNames.CreateCategory), result.Errors));
            return Result<CategoryDto>.Ok(result.Data);
        }
    }
}
