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
        Category GetCatgory(int categoryId);
        ICollection<BookCategory> GetCategoriesOfABook(int bookId);
        ICollection<BookCategory> GetBooksForCategory(int categoryId);
        bool CategoryExists(int categoryId);
    }
}
