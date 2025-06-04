using Domain.Entities;

namespace Bookify_Library_mgnt.Dtos.Categories
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Book> Books { get; set; }
    }
}
