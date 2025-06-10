using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using MediatR;

namespace Application.Categories.Handlers;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CategoryDto>
{
    private readonly ICategoryService _categoryService;

    public DeleteCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<CategoryDto> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        return await _categoryService.DeleteCategoryAsync(command.Id);

    }
}
