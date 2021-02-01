using BookCollectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Services
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();

        Reviewer GetReviewer(int reviewerId);

        ICollection<Review> GetAllReviewsByReviewer(int reviewerId);

        ICollection<Reviewer> GetAllReviewersOfAReview(int reviewId);

        bool ReviewerExists(int reviewerId);
    }
}
