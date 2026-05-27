using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDtos MapToDto(Category category)
        {
            return new CategoryDtos
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        
    }
}
