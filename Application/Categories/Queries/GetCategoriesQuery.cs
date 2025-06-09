using Application.Categories.Dtos;
using MediatR;

namespace Application.Categories.Queries
{
    public record GetCategoriesQuery() : IRequest<List<CategoryDto>>;

}
