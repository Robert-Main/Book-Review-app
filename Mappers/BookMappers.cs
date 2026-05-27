using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class BookMappers
    {
        public static BookDto MapToBookResponseDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ReleaseDate = book.ReleaseDate,
                Rating = book.Reviews?.Any() == true ? book.Reviews.Average(r => r.Rating) : 0,
                Authors = book.BookAuthors?.Select(ba => ba.Author != null ? AuthorMappers.MapToDto(ba.Author) : null).Where(a => a != null).ToList()!,
                Categories = book.BookCategories?.Select(bc => bc.Category != null ? CategoryMappers.MapToDto(bc.Category) : null).Where(c => c != null).ToList()!,
                Reviews = book.Reviews?.Select(r => ReviewMappers.MapToDto(r)).ToList(),
            };
        }

        public static Book MapToEntity(BookDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                ReleaseDate = bookDto.ReleaseDate,
            };
        }
    }
}
