using BookStoreAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreAPI.Services
{
    public class BooksService
    {

        private readonly IMongoCollection<Book> _bookcollection; 
        public BooksService(IOptions<BookStoreDatabase> bookStoreDatabaseSettings)
        {
            var mongoclient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoclient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            var mongoDbCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName); 
        }   
        //CRUD services ! 
        public async Task<List<Book>> GetAsync() => await _bookcollection.Find(_ => true ).ToListAsync();   
        public async Task<Book?> GetBookAsync(string Id ) => await _bookcollection.Find(x => x.ID==Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newBook) => await _bookcollection.InsertOneAsync(newBook);
        public async Task UpdateAsync(String ID, Book newBook) => await _bookcollection.ReplaceOneAsync(x => x.ID == ID, newBook); 

        public async Task DeleteAsync(String Id) => await _bookcollection.DeleteOneAsync(x => x.ID == Id);  

    }
}
