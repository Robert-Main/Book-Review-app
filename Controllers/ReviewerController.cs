using BookReview.Dtos;
using BookReview.interfaces;
using BookReview.Mappers;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerInterface _reviewerRepository;

        public ReviewerController(IReviewerInterface reviewerRepository)
        {
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public async Task<IActionResult> GetReviewers()
        {
            var reviewers = await _reviewerRepository.GetReviewersAsync();
            var reviewersDto = reviewers.Select(r => ReviewerMappers.MapToDto(r)).ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Reviewers retrieved successfully",
                data = reviewersDto
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReviewer(int id)
        {
            if (!await _reviewerRepository.ReviewerExistsAsync(id))
                return NotFound();

            var reviewer = await _reviewerRepository.GetReviewerAsync(id);
            if (reviewer == null)
                return NotFound();

            var reviewerDto = ReviewerMappers.MapToDto(reviewer);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Reviewer retrieved successfully",
                data = reviewerDto
            });
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public async Task<IActionResult> GetReviewsByReviewer(int reviewerId)
        {
            if (!await _reviewerRepository.ReviewerExistsAsync(reviewerId))
                return NotFound();

            var reviews = await _reviewerRepository.GetReviewsByReviewerAsync(reviewerId);
            var reviewsDto = reviews.Select(r => ReviewMappers.MapToDto(r)).ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new
            {
                success = true,
                message = "Reviews by reviewer retrieved successfully",
                data = reviewsDto
            });
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReviewer([FromBody] Reviewer reviewer)
        {
            if (reviewer == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _reviewerRepository.CreateReviewerAsync(reviewer))
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
        public async Task<IActionResult> UpdateReviewer(int id, [FromBody] Reviewer reviewer)
        {
            if (reviewer == null)
                return BadRequest(ModelState);

            if (id != reviewer.Id)
                return BadRequest(ModelState);

            if (!await _reviewerRepository.ReviewerExistsAsync(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _reviewerRepository.UpdateReviewerAsync(reviewer))
            {
                ModelState.AddModelError("", "Something went wrong updating reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReviewer(int id)
        {
            if (!await _reviewerRepository.ReviewerExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _reviewerRepository.DeleteReviewerAsync(id))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
