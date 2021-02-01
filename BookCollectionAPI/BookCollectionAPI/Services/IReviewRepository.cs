using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();

        Review GetReview(int reviewId);

        ICollection<Review> GetAllReviewsOfABook(int bookId);

        ICollection<Book> GetbookOfAReview(int reviewId);

        bool ReviewExists(int reviewId);
    }
}
