namespace BookStoreAPI.Models
{
    public class BookStoreDatabase
    {
        public String? ConnectionString { get; set; }
        public String? DatabaseName { get; set; }
        public String BooksCollectionName { get; set; } = null!;
    }
}
