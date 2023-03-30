using BookStoreAPI.Models;
using BookStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {
      private BooksService _booksService ;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }
        [HttpGet("api/bookstore")]
        public async Task<IEnumerable<Book>> Get () => await  _booksService.GetAsync();
        [HttpGet("api/bookstore/{id}")]
        public async Task<ActionResult<Book>> GetBookAsync(String id) {
            var book = await _booksService.GetBookAsync(id) ;
            if (book is  null)
            {
                return NotFound() ;
            }
            return book; 
        }
        [HttpPost("api/bookstore/{id}")]
        public async Task<IActionResult> InsertBookAsync(Book newBook)
        {
         await   _booksService.CreateAsync(newBook) ;
            return CreatedAtAction(nameof(Get), new { id = newBook.ID },newBook); 
        }
        [HttpPut("api/bookstore/{id}")]
        public async Task<IActionResult> updateBook(String Id , Book updatedbook)
        {
            var book = await _booksService.GetBookAsync(Id) ; 
            if (book is null ) { return NotFound();  }
            updatedbook.ID = book.ID ;  
            await _booksService.UpdateAsync(Id, updatedbook) ;
            return NoContent() ;

        }
        //be careful when specifying the URI , if the smthng is missing , MapController exception will rise !! 
        [HttpDelete("api/bookstore/{id}")]
        public async Task<IActionResult> deleteBook(String id )
        {
            var book = await _booksService.GetBookAsync(id) ; 
            if ( book is null ) { return NotFound(); }  
             await   _booksService.DeleteAsync(id) ;
            return NoContent(); 
        }
    }
}
