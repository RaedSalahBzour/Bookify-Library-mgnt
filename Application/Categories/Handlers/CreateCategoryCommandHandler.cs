using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using AutoMapper;
using MediatR;

namespace Application.Categories.Handlers;

public class CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
    : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<CreateCategoryDto>(command);
        return await categoryService.CreateCategoryAsync(dto);

    }
}
