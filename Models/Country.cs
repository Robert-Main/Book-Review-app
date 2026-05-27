using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Author>? Authors { get; set; } = new List<Author>();
    }
}