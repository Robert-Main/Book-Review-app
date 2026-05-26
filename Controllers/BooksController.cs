
using BookReview.Dtos;
using BookReview.interfaces;
using BookReview.Mappers;
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
            var booksDto = books.Select(b => b.MapToDto()).ToList();
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
                data = booksDto
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookRepository.GetBookAsync(id);
            var bookDto = book.MapToDto();
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
                data = bookDto
            });
        }

        [HttpGet("title/{title}")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var book = await _bookRepository.GetBookByTitle(title);
            var bookDto = book.MapToDto();
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
                data = bookDto
            });
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBookRating(int id)
        {
            var rating = await _bookRepository.GetBookRatingAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid model state"
                });
            return Ok(new
            {
                success = true,
                message = "Book rating retrieved successfully",
                data = rating
            });
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid model state"
                });

            var created = await _bookRepository.CreateBookAsync(book);
            if (!created)
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to create book"
                });

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid model state"
                });

            if (id != book.Id)
                return BadRequest(new
                {
                    success = false,
                    message = "ID mismatch"
                });

            var updated = await _bookRepository.UpdateBookAsync(book);
            if (!updated)
                return NotFound(new
                {
                    success = false,
                    message = "Book not found"
                });

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _bookRepository.DeleteBookAsync(id);
            if (!deleted)
                return NotFound(new
                {
                    success = false,
                    message = "Book not found"
                });

            return NoContent();
        }
    }
}