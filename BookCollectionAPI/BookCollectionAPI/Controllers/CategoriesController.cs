using BookCollectionAPI.Dtos;
using BookCollectionAPI.Models;
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
        private IBookRepository _bookRepository;

        // Constructor
        public CategoriesController(ICategoriesRepository categoriesRepository, IBookRepository bookRepository)
        {
            _categoriesRepository = categoriesRepository;
            _bookRepository = bookRepository;
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
        [HttpGet("{categoryId}", Name = "GetCategory")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CountryDto))]

        public IActionResult GetCategory(int categoryId)
        {
            //check if exist
            if (!_categoriesRepository.CategoryExists(categoryId))
                return NotFound();

            // get category
            var category = _categoriesRepository.GetCategory(categoryId);


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




        
        // api/categories/books/bookId
        [HttpGet("books/{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoriesDto>))]

        public IActionResult GetAllCategoriesForABook(int bookId)
        {
            //check if exist
            if (!_bookRepository.BookExists(bookId))
                return NotFound();

            // get categories
            var categories = _categoriesRepository.GetAllCategoriesOfABook(bookId);


            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriesDto = new List<CategoriesDto>();
            // using categories dto to return only the needed value
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoriesDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }


            return Ok(categoriesDto);
        }




        // api/categories/categoryId/books
        [HttpGet("{categoryId}/books")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]

        public IActionResult GetAllBooksForCategory(int categoryId)
        {
            //check if exist
            if (!_categoriesRepository.CategoryExists(categoryId))
                return NotFound();

            // get books
            var books = _categoriesRepository.GetAllBooksForCategory(categoryId);


            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using categories dto to return only the needed value
            var booksDto = new List<BookDto>();

            foreach (var book in books)
            {
                booksDto.Add(new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Isbn = book.Isbn,
                    DatePublished = book.DatePublished
                });
            }



            return Ok(booksDto);

        }



        //api/categories
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCategory([FromBody] Category categoryToCreate)
        {
            if (categoryToCreate == null)
                return BadRequest(ModelState);

            var category = _categoriesRepository.GetCategories()
                            .Where(c => c.Name.Trim().ToUpper() == categoryToCreate.Name.Trim().ToUpper())
                            .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", $"Category {categoryToCreate.Name} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoriesRepository.CreateCategory(categoryToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {categoryToCreate.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { categoryId = categoryToCreate.Id }, categoryToCreate);
        }

        //api/categories/categoryId
        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] Category updatedCategoryInfo)
        {
            if (updatedCategoryInfo == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategoryInfo.Id)
                return BadRequest(ModelState);

            if (!_categoriesRepository.CategoryExists(categoryId))
                return NotFound();

            if (_categoriesRepository.IsDuplicateCategoryName(categoryId, updatedCategoryInfo.Name))
            {
                ModelState.AddModelError("", $"Category {updatedCategoryInfo.Name} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoriesRepository.UpdateCategory(updatedCategoryInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating {updatedCategoryInfo.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //api/categories/categoryId
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoriesRepository.CategoryExists(categoryId))
                return NotFound();

            var categoryToDelete = _categoriesRepository.GetCategory(categoryId);

            if (_categoriesRepository.GetAllBooksForCategory(categoryId).Count() > 0)
            {
                ModelState.AddModelError("", $"Category {categoryToDelete.Name} " +
                                              "cannot be deleted because it is used by at least one book");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoriesRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {categoryToDelete.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
