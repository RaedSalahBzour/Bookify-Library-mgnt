using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using AutoMapper;
using MediatR;

namespace Application.Categories.Handlers.CommandHandlers;

public class CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
    : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        CreateCategoryDto createCategoryDto = mapper.Map<CreateCategoryDto>(command);
        return await categoryService.CreateCategoryAsync(createCategoryDto);

    }
}
