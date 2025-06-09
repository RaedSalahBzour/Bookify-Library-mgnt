using Application.Categories.Dtos;
using MediatR;

namespace Application.Categories.Queries
{
    public record GetCategoriesQuery(int pageNumber, int pageSize) : IRequest<List<CategoryDto>>;

}
