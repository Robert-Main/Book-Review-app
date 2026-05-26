using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Data;
using BookReview.interfaces;
using BookReview.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Repositories
{
    public class AuthorRepository : IAuthorInterface
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthorExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> CreateAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;
            _context.Authors.Remove(author);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Author> GetAuthorAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<ICollection<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<ICollection<Author>> GetAuthorsOfABookAsync(int bookId)
        {
            return await _context.BookAuthors.Where(ba => ba.BookId == bookId).Select(a => a.Author).ToListAsync();
        }

        public async Task<ICollection<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.BookAuthors.Where(ba => ba.AuthorId == authorId).Select(b => b.Book).ToListAsync();
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
