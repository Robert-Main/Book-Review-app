using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public Country? Country { get; set; }
        public ICollection<BookAuthor>? BookAuthors { get; set; }

    }
}