using BookCollectionAPI.Dtos;
using BookCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : Controller
    {
        private ICountryRepository _countryRepository;

        // Constructor
        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        // api/countries
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200,Type = typeof(IEnumerable<CountryDto>))]
        public IActionResult GetCountries()
        {
            // get countries
            var countries = _countryRepository.GetCountries().ToList();

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using countries dto to return only the needed value
            var countriesDto = new List<CountryDto>();
            foreach(var country in countries)
            {
                countriesDto.Add(new CountryDto
                {
                    Id = country.Id,
                    Name = country.Name
                });
            }

            return Ok(countriesDto);
        }


        // api/countries/countryId
        [HttpGet("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(404)]

        public IActionResult GetCountry(int countryId)
        {
            //check if exist
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            // get country
            var country = _countryRepository.GetCountry(countryId);


            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using countries dto to return only the needed value
            var countryDto = new CountryDto() 
            { 
                Id = country.Id,
                Name = country.Name
            };
         

            return Ok(countryDto);
        }
    }
}
