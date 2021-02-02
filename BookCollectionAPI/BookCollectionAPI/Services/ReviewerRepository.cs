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
            return _reviewerContext.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _reviewerContext.Reviewers.OrderBy(r => r.FirstName).ToList();
        }

        public Reviewer GetReviewerOfAReview(int reviewId)
        {
            // Get the reviwer ID where is matches the arg in the reviews
            var reviewerId = _reviewerContext.Reviews.Where(r => r.Id == reviewId).Select(rr => rr.Reviewer.Id).FirstOrDefault();

            // Now use the reviewerID to find the reviewers
            return _reviewerContext.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();


        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _reviewerContext.Reviews.Where(r => r.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _reviewerContext.Reviewers.Any(r => r.Id == reviewerId);
        }
    }
}
