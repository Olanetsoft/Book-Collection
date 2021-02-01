using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();

        Book GetBookById(int bookId);

        Book GetBookByISBN(int isbn);


        bool BookExistsById(int reviewerId);

        bool BookExistsByISBN(int isbn);

        bool IsDuplicateISBN(int isbn);

        decimal GetBookRating(int bookId);
    }
}
