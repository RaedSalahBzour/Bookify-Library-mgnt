using Application.Categories.Dtos;
using Application.Categories.Queries;
using Application.Categories.Services;
using MediatR;

namespace Application.Categories.Handlers;

public class GetCategoriesQueryHandler(ICategoryService categoryService)
    : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        return await categoryService.GetCategoriesAsync();

    }
}
