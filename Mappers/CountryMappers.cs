using BookReview.Dtos;
using BookReview.Models;

namespace BookReview.Mappers
{
    public static class CountryMappers
    {
        public static CountryDtos MapToDto(this Country country)
        {
            return new CountryDtos
            {
                Id = country.Id,
                Name = country.Name
            };
        }

        public static Country MapToEntity(this CountryDtos countryDto)
        {
            return new Country
            {
                Id = countryDto.Id ?? 0,
                Name = countryDto.Name
            };
        }
    }
}
