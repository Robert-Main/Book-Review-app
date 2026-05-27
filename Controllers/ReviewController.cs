using BookReview.Dtos;
using BookReview.interfaces;
using BookReview.Mappers;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewInterface _reviewRepository;

        public ReviewController(IReviewInterface reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await _reviewRepository.GetReviewsAsync();
            var reviewsDto = reviews.Select(r => ReviewMappers.MapToDto(r)).ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Reviews retrieved successfully",
                data = reviewsDto
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReview(int id)
        {
            if (!await _reviewRepository.ReviewExistsAsync(id))
                return NotFound();

            var review = await _reviewRepository.GetReviewAsync(id);
            if (review == null)
                return NotFound();

            var reviewDto = ReviewMappers.MapToDto(review);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Review retrieved successfully",
                data = reviewDto
            });
        }

        [HttpGet("book/{bookId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public async Task<IActionResult> GetReviewsOfABook(int bookId)
        {
            var reviews = await _reviewRepository.GetReviewsOfABookAsync(bookId);
            var reviewsDto = reviews.Select(r => ReviewMappers.MapToDto(r)).ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Reviews of book retrieved successfully",
                data = reviewsDto
            });
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            if (review == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _reviewRepository.CreateReviewAsync(review))
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
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review review)
        {
            if (review == null)
                return BadRequest(ModelState);

            if (id != review.Id)
                return BadRequest(ModelState);

            if (!await _reviewRepository.ReviewExistsAsync(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _reviewRepository.UpdateReviewAsync(review))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (!await _reviewRepository.ReviewExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _reviewRepository.DeleteReviewAsync(id))
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
