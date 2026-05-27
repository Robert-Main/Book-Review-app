using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class ReviewerMappers
    {
        public static ReviewerDto MapToDto(Reviewer reviewer)
        {
            return new ReviewerDto
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };
        }

        public static Reviewer MapToEntity(ReviewerDto reviewerDto)
        {
            return new Reviewer
            {
                FirstName = reviewerDto.FirstName,
                LastName = reviewerDto.LastName
            };
        }
    }
}
