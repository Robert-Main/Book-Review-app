using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; } = new List<BookCategory>();
    }
}