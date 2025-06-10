using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using AutoMapper;
using MediatR;

namespace Application.Categories.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    public async Task<CategoryDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<CreateCategoryDto>(command);
        return await _categoryService.CreateCategoryAsync(dto);

    }
}
