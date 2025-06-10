using Application.Categories.Dtos;
using Application.Categories.Queries;
using Application.Categories.Services;
using MediatR;

namespace Application.Categories.Handlers.QeuryHandlers;

public class GetCategoryByIdQueryHandler(ICategoryService categoryService)
    : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        return await categoryService.GetByIdAsync(query.id);

    }
}
