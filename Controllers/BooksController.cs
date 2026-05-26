
using BookReview.interfaces;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBook _bookRepository;
        public BooksController(IBook bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetBooksAsync();
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid model state"
                });
            return Ok(new
            {
                success = true,
                message = "Books retrieved successfully",
                data = books
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookRepository.GetBookAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid model state"
                });

            if (book == null)
                return NotFound(new
                {
                    success = false,
                    message = "Book not found"
                });
            return Ok(new
            {
                success = true,
                message = "Book retrieved successfully",
                data = book
            });
        }

        [HttpGet("title/{title}")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var book = await _bookRepository.GetBookByTitle(title);
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid model state"
                });

            if (book == null)
                return NotFound(new
                {
                    success = false,
                    message = "Book not found"
                });
            return Ok(new
            {
                success = true,
                message = "Book retrieved successfully",
                data = book
            });
        }
    }
}