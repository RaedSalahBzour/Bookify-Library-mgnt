using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using MediatR;

namespace Application.Categories.Handlers.CommandHandlers;

public class DeleteCategoryCommandHandler(ICategoryService categoryService)
    : IRequestHandler<DeleteCategoryCommand, CategoryDto>
{

    public async Task<CategoryDto> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        return await categoryService.DeleteCategoryAsync(command.Id);

    }
}
