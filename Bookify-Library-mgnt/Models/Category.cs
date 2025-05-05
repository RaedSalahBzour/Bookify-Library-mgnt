using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bookify_Library_mgnt.Models
{
    public class Category
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Book> Books { get; set; }
        [JsonIgnore]
        public List<CategoryBook> CategoryBooks { get; set; }
    }
}
