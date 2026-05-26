using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDto MapToDto(this Review review)
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
    }
}
