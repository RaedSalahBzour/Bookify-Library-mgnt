using System.Text.Json.Serialization;

namespace Data.Entities;

public class Category
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<Book> Books { get; set; }

    [JsonIgnore]
    public List<CategoryBook> CategoryBooks { get; set; }
}
