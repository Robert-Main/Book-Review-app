using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Models;

namespace BookReview.interfaces
{
    public interface IAuthorInterface
    {
        Task<ICollection<Author>> GetAuthorsAsync();
        Task<Author?> GetAuthorAsync(int id);
        Task<ICollection<Author>> GetAuthorsOfABookAsync(int bookId);
        Task<ICollection<Book>> GetBooksByAuthorAsync(int authorId);
        Task<bool> AuthorExistsAsync(int id);
        Task<bool> CreateAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
