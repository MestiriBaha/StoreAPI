using BookStoreAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreAPI.Services
{
    public class BooksService
    {

        private readonly IMongoCollection<Book> _bookcollection ; 
        public BooksService(IOptions<BookStoreDatabase> bookStoreDatabaseSettings)
        {
            var mongoclient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionURI);
            var mongoDatabase = mongoclient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
             _bookcollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.CollectionName); 
        }
        //CRUD services ! 
        public async Task<IEnumerable<Book>> GetAsync()
        {
            var filter = Builders<Book>.Filter.Empty;
           // var options = new FindOptions<Book>();
            return  await _bookcollection.Find(filter).ToListAsync();
        } 
        public async Task<Book?> GetBookAsync(string Id ) => await _bookcollection.Find(x => x.ID==Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newBook) => await _bookcollection.InsertOneAsync(newBook);
        public async Task UpdateAsync(String ID, Book newBook) => await _bookcollection.ReplaceOneAsync(x => x.ID == ID, newBook); 

        public async Task DeleteAsync(String Id) => await _bookcollection.DeleteOneAsync(x => x.ID == Id);  

    }
}
