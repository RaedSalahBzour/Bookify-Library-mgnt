using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using AutoMapper;
using MediatR;

namespace Application.Categories.Handlers;

public class UpdateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
    : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{

    public async Task<CategoryDto> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<UpdateCategoryDto>(command);
        return await categoryService.UpdateCategoryAsync(command.id, dto);

    }
}
