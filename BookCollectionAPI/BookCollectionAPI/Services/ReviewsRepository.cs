using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public class ReviewsRepository : IReviewRepository
    {
        // Create a private variable to get access to db context
        private BookDBContext _reviewContext;

        // a constructor
        public ReviewsRepository(BookDBContext reviewContext)
        {
            _reviewContext = reviewContext;
        }

        public Book GetbookOfAReview(int reviewId)
        {
            // Get the book ID where is matches the arg in the reviews
            var bookId = _reviewContext.Reviews.Where(r => r.Id == reviewId).Select(rr => rr.Book.Id).FirstOrDefault();

            // Now use the bookID to find the book
            return _reviewContext.Books.Where(r => r.Id == bookId).FirstOrDefault();
        }

        public Review GetReview(int reviewId)
        {
            return _reviewContext.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _reviewContext.Reviews.OrderBy(r => r.Rating).ToList();
        }

        public ICollection<Review> GetReviewsOfABook(int bookId)
        {
            return _reviewContext.Reviews.Where(b => b.Book.Id == bookId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _reviewContext.Reviews.Any(r => r.Id == reviewId);

        }
    }
}
