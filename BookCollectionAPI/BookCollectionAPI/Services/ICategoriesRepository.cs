using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public interface ICategoriesRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);
        ICollection<Category> GetAllCategoriesOfABook(int bookId);
        ICollection<Book> GetBooksForCategory(int categoryId);
        bool CategoryExists(int categoryId);
    }
}
