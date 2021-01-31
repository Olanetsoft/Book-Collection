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
    public class CategoriesController : Controller
    {
        private ICategoriesRepository _categoriesRepository;

        // Constructor
        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        // api/categories
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoriesDto>))]
        public IActionResult GetCategories()
        {
            // get categories
            var countries = _categoriesRepository.GetCategories().ToList();

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using categories dto to return only the needed value
            var categoriesDto = new List<CategoriesDto>();
            foreach (var category in countries)
            {
                categoriesDto.Add(new CategoriesDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoriesDto);
        }

        // api/categories/categoryId
        [HttpGet("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(404)]

        public IActionResult GetCategory(int categoryId)
        {
            //check if exist
            if (!_categoriesRepository.CategoryExists(categoryId))
                return NotFound();

            // get category
            var category = _categoriesRepository.GetCatgory(categoryId);


            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using categories dto to return only the needed value
            var categorieDto = new CategoriesDto()
            {
                Id = category.Id,
                Name = category.Name
            };


            return Ok(categorieDto);
        }

        // api/categories/books/categoryId
        [HttpGet("books/{categoryId}")]
        [ProducesResponseType(400)]
        //[ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(404)]

        public IActionResult GetBooksForCategory(int categoryId)
        {
            ////check if exist
            //if (!_countryRepository.CountryExists(countryId))
            //    return NotFound();

            // get country
            var books = _categoriesRepository.GetBooksForCategory(categoryId);


            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //// using categories dto to return only the needed value
            //var countryDto = new CountryDto()
            //{
            //    Id = books.Id,
            //    Name = books.Name
            //};


            return Ok(books);

        }
}
