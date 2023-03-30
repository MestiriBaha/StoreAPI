namespace BookStoreAPI.Models
{
    public class BookStoreDatabase
    {
        public String? ConnectionURI { get; set; }
        public String? DatabaseName { get; set; }
        public String CollectionName { get; set; } = null!;
    }
}
