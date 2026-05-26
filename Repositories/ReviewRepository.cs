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
    public class ReviewRepository : IReviewInterface
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;
            _context.Reviews.Remove(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Review> GetReviewAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ICollection<Review>> GetReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsOfABookAsync(int bookId)
        {
            return await _context.Reviews.Where(r => r.Book.Id == bookId).ToListAsync();
        }

        public async Task<bool> ReviewExistsAsync(int id)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
