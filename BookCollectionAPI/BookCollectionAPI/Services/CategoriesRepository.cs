using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public class CategoriesRepository : ICategoriesRepository
    {
        // Create a private variable to get access to db context
        private BookDBContext _categoryContext;

        // a constructor
        public CategoriesRepository(BookDBContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        public bool CategoryExists(int categoryId)
        {
            return _categoryContext.Countries.Any(c => c.Id == categoryId);
        }

        public ICollection<Category> GetCategories()
        {
            return _categoryContext.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _categoryContext.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
        }

        ICollection<Book> ICategoriesRepository.GetAllBooksForCategory(int categoryId)
        {
            return _categoryContext.BookCategories.Where(c => c.CategoryId == categoryId).Select(b => b.Book).ToList();
        }

        ICollection<Category> ICategoriesRepository.GetAllCategoriesOfABook(int bookId)
        {
            return _categoryContext.BookCategories.Where(b => b.BookId == bookId).Select(c => c.Category).ToList();
        }
    }
}
