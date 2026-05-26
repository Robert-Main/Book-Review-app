using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class ReviewerMappers
    {
        public static ReviewerDto MapToDto(this Reviewer reviewer)
        {
            return new ReviewerDto
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };
        }
    }
}
