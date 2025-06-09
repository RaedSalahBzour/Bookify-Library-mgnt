using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using Bookify_Library_mgnt.Common;
using Domain.Enums;
using Domain.Shared;
using MediatR;

namespace Application.Categories.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Result<CategoryDto>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _categoryService.DeleteCategoryAsync(command.Id);
            if (!result.IsSuccess)
                return Result<CategoryDto>.Fail(ErrorMessages
                    .OperationFailed(nameof(OperationNames.DeleteCategory), result.Errors));
            return Result<CategoryDto>.Ok(result.Data);
        }
    }
}
