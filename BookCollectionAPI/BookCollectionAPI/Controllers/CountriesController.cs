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
        private IAuthorRepository _authorRepository;

        // Constructor
        public CountriesController(ICountryRepository countryRepository, IAuthorRepository authorRepository)
        {
            _countryRepository = countryRepository;
            _authorRepository = authorRepository;
        }



        // GET Countries
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



        // GET a Country
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



        // GET Country of an author
        // api/countries/authors/authorId
        [HttpGet("authors/{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(404)]

        public IActionResult GetCountryOfAnAuthor(int authorId)
        {
            //check if exist
            if (!_authorRepository.AuthorExists(authorId))
                return NotFound();

            // get country
            var country = _countryRepository.GetCountryOfAnAuthor(authorId);


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



        // GET Authors From a Country
        // api/countries/countryId/authors
        [HttpGet("{countryId}/authors")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]

        public IActionResult GetAuthorsFromACountry(int countryId)
        {
            //check if exist
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            // get authors
            var authors = _countryRepository.GetAuthorsFromACountry(countryId);


            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using authors dto to return only the needed value
            var authorsDto = new List<AuthorDto>();

            foreach(var author in authors)
            {
                authorsDto.Add(new AuthorDto
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                });
            }


            return Ok(authorsDto);
        }

    }
}
