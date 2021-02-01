using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public interface IAuthorRepository
    {
        ICollection<Author> GetAuthors();

        Author GetAuthor(int authorId);

        ICollection<Author> GetAuthorsOfABook(int bookId);

        ICollection<Book> GetBooksByAuthor(int authorId);

        bool AuthorExists(int authorId);
    }
}
