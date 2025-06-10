using System.Text.Json.Serialization;
namespace Data.Entities;

public class Book
{
    public string Id { get; set; }
    public string Author { get; set; }
    public string? Description { get; set; }
    public string Title { get; set; }
    public string? CoverUrl { get; set; }
    public string ISBN { get; set; }
    public DateTime PublishDate { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Borrowing> Borrowings { get; set; }

    [JsonIgnore]
    public List<UserBook> UserBooks { get; set; }

    [JsonIgnore]
    public List<CategoryBook> CategoryBooks { get; set; }
}

