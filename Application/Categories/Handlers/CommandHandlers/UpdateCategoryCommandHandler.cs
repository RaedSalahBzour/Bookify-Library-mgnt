using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using AutoMapper;
using MediatR;

namespace Application.Categories.Handlers.CommandHandlers;

public class UpdateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
    : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{

    public async Task<CategoryDto> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        UpdateCategoryDto updateCategoryDto = mapper.Map<UpdateCategoryDto>(command);
        return await categoryService.UpdateCategoryAsync(command.id, updateCategoryDto);

    }
}
