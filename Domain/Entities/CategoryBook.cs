namespace Data.Entities
{
    public class CategoryBook
    {
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
