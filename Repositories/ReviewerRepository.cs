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
    public class ReviewerRepository : IReviewerInterface
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateReviewerAsync(Reviewer reviewer)
        {
            _context.Reviewers.Add(reviewer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteReviewerAsync(int id)
        {
            var reviewer = await _context.Reviewers.FindAsync(id);
            if (reviewer == null) return false;
            _context.Reviewers.Remove(reviewer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Reviewer?> GetReviewerAsync(int id)
        {
            return await _context.Reviewers.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ICollection<Reviewer>> GetReviewersAsync()
        {
            return await _context.Reviewers.ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsByReviewerAsync(int reviewerId)
        {
            return await _context.Reviews.Where(r => r.Reviewer != null && r.Reviewer.Id == reviewerId).ToListAsync();
        }

        public async Task<bool> ReviewerExistsAsync(int id)
        {
            return await _context.Reviewers.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> UpdateReviewerAsync(Reviewer reviewer)
        {
            _context.Reviewers.Update(reviewer);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
