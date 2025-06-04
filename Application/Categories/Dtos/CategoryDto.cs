using Domain.Entities;

namespace Application.Categories.Dtos
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Domain.Entities.Book> Books { get; set; }
    }
}
