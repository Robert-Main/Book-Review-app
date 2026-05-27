using BookReview.Dtos;
using BookReview.interfaces;
using BookReview.Mappers;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorInterface _authorRepository;

        public AuthorController(IAuthorInterface authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorRepository.GetAuthorsAsync();
            var authorsDto = authors.Select(a => AuthorMappers.MapToDto(a)).ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Authors retrieved successfully",
                data = authorsDto
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Author))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAuthor(int id)
        {
            if (!await _authorRepository.AuthorExistsAsync(id))
                return NotFound(new { success = false, message = "Author not found" });

            var author = await _authorRepository.GetAuthorAsync(id);
            var authorDto = AuthorMappers.MapToDto(author);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Author retrieved successfully",
                data = authorDto
            });
        }

        [HttpGet("book/{bookId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        public async Task<IActionResult> GetAuthorsOfABook(int bookId)
        {
            var authors = await _authorRepository.GetAuthorsOfABookAsync(bookId);
            var authorsDto = authors.Select(a => AuthorMappers.MapToDto(a)).ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Authors of book retrieved successfully",
                data = authorsDto
            });
        }

        [HttpGet("{authorId}/books")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            if (!await _authorRepository.AuthorExistsAsync(authorId))
                return NotFound(new { success = false, message = "Author not found" });

            var books = await _authorRepository.GetBooksByAuthorAsync(authorId);
            var booksDto = books.Select(b => BookMappers.MapToBookResponseDto(b)).ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Books by author retrieved successfully",
                data = booksDto
            });
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            if (author == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _authorRepository.CreateAuthorAsync(author))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { success = true, message = "Successfully created" });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author author)
        {
            if (author == null)
                return BadRequest(ModelState);

            if (id != author.Id)
                return BadRequest(ModelState);

            if (!await _authorRepository.AuthorExistsAsync(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _authorRepository.UpdateAuthorAsync(author))
            {
                ModelState.AddModelError("", "Something went wrong updating author");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (!await _authorRepository.AuthorExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _authorRepository.DeleteAuthorAsync(id))
            {
                ModelState.AddModelError("", "Something went wrong deleting author");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
