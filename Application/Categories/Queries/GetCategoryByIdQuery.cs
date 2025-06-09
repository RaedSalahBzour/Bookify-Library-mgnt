using Application.Categories.Dtos;
using MediatR;

namespace Application.Categories.Queries
{
    public record GetCategoryByIdQuery(string id) : IRequest<CategoryDto>;
}
