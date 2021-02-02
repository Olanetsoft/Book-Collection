using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public class BookRepository : IBookRepository
    {

        private BookDBContext _bookDbContext;

        public BookRepository(BookDBContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        public bool BookExists(int bookId)
        {
            return _bookDbContext.Books.Any(b => b.Id == bookId);
        }

        public bool BookExists(string isbn)
        {
            return _bookDbContext.Books.Any(b => b.Isbn == isbn);
        }

        public Book GetBook(int bookId)
        {
            return _bookDbContext.Books.Where(b => b.Id == bookId).FirstOrDefault();
        }

        public Book GetBook(string isbn)
        {
            return _bookDbContext.Books.Where(b => b.Isbn == isbn).FirstOrDefault();
        }

        public decimal GetBookRating(int bookId)
        {
            var reviews = _bookDbContext.Reviews.Where(r => r.Book.Id == bookId);

            if (reviews.Count() <= 0)
                return 0;

            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<Book> GetBooks()
        {
            return _bookDbContext.Books.OrderBy(b => b.Title).ToList();
        }

        public bool IsDuplicateISBN(int bookId, string isbn)
        {
            var book = _bookDbContext.Books.Where(b => b.Isbn.Trim().ToUpper() == isbn.Trim().ToUpper()
                                                && b.Id != bookId).FirstOrDefault();

            return book == null ? false : true;
        }
    }
}
