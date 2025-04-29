namespace Bookify_Library_mgnt.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Book> Books { get; set; }
        public List<CategoryBook> CategoryBooks { get; set; }
    }
}
