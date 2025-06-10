using Application.Categories.Dtos;
using Application.Categories.Queries;
using Application.Categories.Services;
using MediatR;

namespace Application.Categories.Handlers;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoriesQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        return await _categoryService.GetCategoriesAsync();

    }
}
