using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Models;

namespace BookReview.interfaces
{
    public interface IReviewInterface
    {
        Task<ICollection<Review>> GetReviewsAsync();
        Task<Review?> GetReviewAsync(int id);
        Task<ICollection<Review>> GetReviewsOfABookAsync(int bookId);
        Task<bool> ReviewExistsAsync(int id);
        Task<bool> CreateReviewAsync(Review review);
        Task<bool> UpdateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(int id);
    }
}
