using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Models;

namespace BookReview.interfaces
{
    public interface IReviewerInterface
    {
        Task<ICollection<Reviewer>> GetReviewersAsync();
        Task<Reviewer?> GetReviewerAsync(int id);
        Task<ICollection<Review>> GetReviewsByReviewerAsync(int reviewerId);
        Task<bool> ReviewerExistsAsync(int id);
        Task<bool> CreateReviewerAsync(Reviewer reviewer);
        Task<bool> UpdateReviewerAsync(Reviewer reviewer);
        Task<bool> DeleteReviewerAsync(int id);
    }
}
