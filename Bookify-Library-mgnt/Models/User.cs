using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Bookify_Library_mgnt.Models
{
    public class User : IdentityUser
    {
        public List<Review> Reviews { get; set; }
        public List<Borrowing> Borrowings { get; set; }
        [JsonIgnore]
        public List<UserBook> UserBooks { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }


    }
}
