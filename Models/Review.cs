using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int Rating { get; set; }

        public DateTime ReviewDate { get; set; }

        public string? ReviewerName { get; set; }

        public Book? Book { get; set; }

        public Reviewer? Reviewer { get; set; }
    }
}