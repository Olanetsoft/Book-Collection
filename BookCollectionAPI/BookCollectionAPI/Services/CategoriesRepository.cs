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

        public ICollection<BookCategory> GetBooksForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Category> GetCategories()
        {
            return _categoryContext.Categories.OrderBy(c => c.Name).ToList();
        }

        public ICollection<BookCategory> GetCategoriesOfABook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Category GetCatgory(int categoryId)
        {
            return _categoryContext.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
        }
    }
}
