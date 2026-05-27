using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class AuthorMappers
    {
        public static AuthorDto MapToDto(Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                Country = author.Country != null ? CountryMappers.MapToDto(author.Country) : null
            };
        }

        public static Author MapToEntity(AuthorDto authorDto)
        {
            return new Author
            {
                Name = authorDto.Name,
                Bio = authorDto.Bio,
            };
        }
    }
}
