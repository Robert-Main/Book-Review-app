using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Models;

namespace BookReview.interfaces
{
    public interface ICategoryInterface
    {
        public Task<ICollection<Category>> GetCategoriesAsync();
        public Task<Category?> GetCategoryAsync(int id);
        public Task<bool> CreateCategoryAsync(Category category);
        public Task<bool> UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int id);
    }
}