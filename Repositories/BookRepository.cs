using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Data;
using BookReview.interfaces;
using BookReview.Models;

namespace BookReview.Repositories
{
    public class BookRepository:IBook
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Book>> GetBooksAsync()
        {
            return await Task.FromResult(_context.Books.OrderBy(b => b.Id).ToList());
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await Task.FromResult(_context.Books.FirstOrDefault(b => b.Id == id));
        }

        public async Task<Book> GetBookByTitle(string title)
        {
            return await Task.FromResult(_context.Books.FirstOrDefault(b => b.Title.ToLower() == title.ToLower()));
        }

        public async Task<double> GetBookRatingAsync(int id)
        {
            var reviews = _context.Reviews.Where(r => r.Book.Id == id).ToList();
            if (reviews.Count == 0)                return 0;
            return reviews.Average(r => r.Rating);
        }
    }
}
