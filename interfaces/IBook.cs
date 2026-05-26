using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Models;

namespace BookReview.interfaces
{
    public interface IBook
    {
        Task<ICollection<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task<Book> GetBookByTitle(string title);
        Task<double> GetBookRatingAsync(int id);

    }
}