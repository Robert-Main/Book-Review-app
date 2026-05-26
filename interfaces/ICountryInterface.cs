using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Models;

namespace BookReview.interfaces
{
    public interface ICountryInterface
    {
        public Task<ICollection<Country>> GetCountriesAsync();
        public Task<Country> GetCountryAsync(int id);
        public Task<bool> CreateCountryAsync(Country country);
        public Task<bool> UpdateCountryAsync(Country country);
        public Task<bool> DeleteCountryAsync(int id);
    }
}