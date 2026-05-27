using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Models;
using BookReview.Helper;
namespace BookReview.interfaces
{
    public interface IBook
    {
        Task<ICollection<Book>> GetBooksAsync(QueryObject query);
        Task<Book?> GetBookAsync(int id);
        Task<Book?> GetBookByTitle(string title);
        Task<double> GetBookRatingAsync(int id);

        Task<bool> BookExistsAsync(int id);
        Task<bool> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);

    }
}