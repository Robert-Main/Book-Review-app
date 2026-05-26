using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Dtos;
using BookReview.interfaces;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryInterface _countryRepository;
        public CountryController(ICountryInterface countryRepository)
        {
            _countryRepository = countryRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCountries()
        {
            var countries = await _countryRepository.GetCountriesAsync();
            return Ok(new
            {
                success = true,
                message = "Countries retrieved successfully",
                data = new
                {
                    countries = countries.Select(c => new CountryDtos
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()
                }
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCountry(int id)
        {
            var country = await _countryRepository.GetCountryAsync(id);
            if (country == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Country not found"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Country retrieved successfully",
                data = new CountryDtos
                {
                    Id = country.Id,
                    Name = country.Name
                }
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateCountry(CountryDtos countryDto)
        {
            var country = new Country
            {
                Name = countryDto.Name
            };

            var result = await _countryRepository.CreateCountryAsync(country);
            if (!result)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to create country"
                });
            }

            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, new
            {
                success = true,
                message = "Country created successfully",
                data = countryDto
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCountry(int id, CountryDtos countryDto)
        {
            var country = await _countryRepository.GetCountryAsync(id);
            if (country == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Country not found"
                });
            }

            country.Name = countryDto.Name;

            var result = await _countryRepository.UpdateCountryAsync(country);
            if (!result)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to update country"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Country updated successfully",
                data = countryDto
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCountry(int id)
        {
            var country = await _countryRepository.GetCountryAsync(id);
            if (country == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Country not found"
                });
            }

            var result = await _countryRepository.DeleteCountryAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to delete country"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Country deleted successfully"
            });
        }
    }
}