using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public bool CategoryExists(int categoryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<BookCategory> GetBooksForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public ICollection<BookCategory> GetCategoriesOfABook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Category GetCatgory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
