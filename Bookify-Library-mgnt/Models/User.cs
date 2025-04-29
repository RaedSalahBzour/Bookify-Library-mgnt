using Microsoft.AspNetCore.Identity;

namespace Bookify_Library_mgnt.Models
{
    public class User : IdentityUser
    {
        public List<Review> Reviews { get; set; }
        public List<Borrowing> Borrowings { get; set; }
        public List<UserBook> UserBooks { get; set; }


    }
}
