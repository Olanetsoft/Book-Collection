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

        Book GetBook(int bookId);

        Book GetBook(string isbn);

        decimal GetBookRating(int bookId);

        bool BookExists(int bookId);

        bool BookExists(string isbn);

        bool IsDuplicateISBN(int bookId, string isbn);

    }
}
