using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.Helper
{
    public class QueryObject
    {
        public string? Author { get; set; }
        public string? ReviewName { get; set; }
        public string? Category { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
    }
}