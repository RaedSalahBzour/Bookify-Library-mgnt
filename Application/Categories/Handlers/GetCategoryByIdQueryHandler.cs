using Application.Categories.Dtos;
using Application.Categories.Queries;
using Application.Categories.Services;
using MediatR;

namespace Application.Categories.Handlers;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryByIdQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        return await _categoryService.GetByIdAsync(query.id);

    }
}
