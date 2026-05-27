using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDto MapToDto(Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                Title = review.Title,
                Content = review.Content,
                Rating = review.Rating,
                ReviewDate = review.ReviewDate,
                ReviewerName = review.ReviewerName
            };
        }

        public static Review MapToEntity(ReviewDto reviewDto)
        {
            return new Review
            {
                Title = reviewDto.Title,
                Content = reviewDto.Content,
                Rating = reviewDto.Rating,
                ReviewDate = reviewDto.ReviewDate,
                ReviewerName = reviewDto.ReviewerName
            };
        }
    }
}
