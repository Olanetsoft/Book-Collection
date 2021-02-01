using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public class ReviewerRepository : IReviewerRepository
    {
        // Create a private variable to get access to db context
        private BookDBContext _reviewerContext;

        // a constructor
        public ReviewerRepository(BookDBContext reviewerContext)
        {
            _reviewerContext = reviewerContext;
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _reviewerContext.Reviewers.Where(c => c.Id == reviewerId).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _reviewerContext.Reviewers.OrderBy(c => c.FirstName).ToList();
        }

        public Reviewer GetReviewersOfAReview(int reviewId)
        {
            return _reviewerContext.Reviews.Where(c => c.Id == reviewId).Select(r => r.Reviewer).FirstOrDefault();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _reviewerContext.Reviews.Where(c => c.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _reviewerContext.Reviewers.Any(c => c.Id == reviewerId);
        }
    }
}
