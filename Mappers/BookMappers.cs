using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class BookMappers
    {
        public static BookDto MapToDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ReleaseDate = book.ReleaseDate,
                Rating = book.Reviews?.Any() == true ? book.Reviews.Average(r => r.Rating) : 0,
                Authors = book.BookAuthors?.Select(ba => ba.Author?.MapToDto()).Where(a => a != null).ToList()!,
                Categories = book.BookCategories?.Select(bc => bc.Category?.MapToDto()).Where(c => c != null).ToList()!,
                Reviews = book.Reviews?.Select(r => r.MapToDto()).ToList()
            };
        }
    }
}
