using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using AutoMapper;
using MediatR;

namespace Application.Categories.Handlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    public UpdateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    public async Task<CategoryDto> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<UpdateCategoryDto>(command);
        return await _categoryService.UpdateCategoryAsync(command.id, dto);

    }
}
