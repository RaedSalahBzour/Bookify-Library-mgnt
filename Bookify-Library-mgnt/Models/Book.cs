using Bookify_Library_mgnt.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Bookify_Library_mgnt.Models
{
    public class Book
    {

        public string Id { get; set; }
        [Required]
        public string Author { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Title { get; set; }
        public string? CoverUrl { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Borrowing> Borrowings { get; set; }
        [JsonIgnore]
        public List<UserBook> UserBooks { get; set; }
        [JsonIgnore]
        public List<CategoryBook> CategoryBooks { get; set; } = new();

    }
}

