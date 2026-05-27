using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Data;
using BookReview.Helper;
using BookReview.interfaces;
using BookReview.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Repositories
{
    public class BookRepository:IBook
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Book>> GetBooksAsync(QueryObject query)
        {
            var books=_context.Books.Include(b => b.Reviews).Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                        .ThenInclude(a => a.Country)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .AsQueryable();

                if (!string.IsNullOrEmpty(query.Author))
                {
                    books = books.Where(b => b.BookAuthors != null && b.BookAuthors.Any(ba => ba.Author != null && ba.Author.Name != null && ba.Author.Name.ToLower().Contains(query.Author.ToLower())));
                }

                if (!string.IsNullOrEmpty(query.ReviewName))
                {
                    books = books.Where(b => b.Reviews != null && b.Reviews.Any(r => r.ReviewerName != null && r.ReviewerName.ToLower().Contains(query.ReviewName.ToLower())));
                }

                if (!string.IsNullOrEmpty(query.Category))
                {
                    books = books.Where(b => b.BookCategories != null && b.BookCategories.Any(bc => bc.Category != null && bc.Category.Name != null && bc.Category.Name.ToLower().Contains(query.Category.ToLower())));
                }

                if (!string.IsNullOrEmpty(query.SortBy))
                {
                    switch (query.SortBy)
                    {
                        case "title":
                            books = query.IsDescending ? books.OrderByDescending(b => b.Title) : books.OrderBy(b => b.Title);
                            break;
                        case "releaseDate":
                            books = query.IsDescending ? books.OrderByDescending(b => b.ReleaseDate) : books.OrderBy(b => b.ReleaseDate);
                            break;
                    }
                }

                return await books.ToListAsync();
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Reviews)!
                .Include(b => b.BookAuthors)!
                    .ThenInclude(ba => ba.Author)!
                        .ThenInclude(a => a!.Country)
                .Include(b => b.BookCategories)!
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetBookByTitle(string title)
        {
            return await _context.Books
                .Include(b => b.Reviews)!
                .Include(b => b.BookAuthors)!
                    .ThenInclude(ba => ba.Author)!
                        .ThenInclude(a => a!.Country)
                .Include(b => b.BookCategories)!
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.Title != null && b.Title.ToLower() == title.ToLower());
        }

        public async Task<double> GetBookRatingAsync(int id)
        {
            var reviews = _context.Reviews.Where(r => r.Book.Id == id).ToList();
            if (reviews.Count == 0)                return 0;
            return reviews.Average(r => r.Rating);
        }


        public async Task<bool> BookExistsAsync(int id)
        {
            return await Task.FromResult(_context.Books.Any(b => b.Id == id));
        }

        public async Task<bool> CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            var existingBook = await GetBookAsync(book.Id);
            if (existingBook == null) return false;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await GetBookAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
