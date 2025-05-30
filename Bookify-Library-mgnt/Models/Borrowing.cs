﻿using System.ComponentModel.DataAnnotations;

namespace Bookify_Library_mgnt.Models
{
    public class Borrowing
    {
        public string Id { get; set; }
        [Required]
        public DateTime BorrowedOn { get; set; }
        [Required]
        public DateTime ReturnedOn { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }

    }
}
