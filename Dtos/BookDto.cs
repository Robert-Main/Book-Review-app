using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        public List<AuthorDto>? Authors { get; set; }
        public List<CategoryDtos>? Categories { get; set; }
        public List<ReviewDto>? Reviews { get; set; }

    }
}