using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class AuthorMappers
    {
        public static AuthorDto MapToDto(this Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                Country = author.Country?.MapToDto()
            };
        }
    }
}
