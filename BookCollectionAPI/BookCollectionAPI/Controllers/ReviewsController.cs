using BookCollectionAPI.Dtos;
using BookCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ReviewsController : Controller
    {
        private IReviewerRepository _reviewerRepository;
        private IReviewRepository _reviewRepository;
        private IBookRepository _bookRepository;

        // Constructor
        public ReviewsController(IReviewerRepository reviewerRepository, IReviewRepository reviewRepository, IBookRepository bookRepository)
        {
            _reviewerRepository = reviewerRepository;
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
        }




        // api/reviewes
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviews()
        {
            // get reviews
            var reviews = _reviewRepository.GetReviews().ToList();

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using reviews dto to return only the needed value
            var reviewsDto = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewsDto.Add(new ReviewDto
                {
                    Id = review.Id,
                    Headline = review.Headline,
                    ReviewText = review.ReviewText,
                    Rating =review.Rating
                });
            }

            return Ok(reviewsDto);
        }



        // api/reviews/reviewId
        [HttpGet("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        public IActionResult GetReview(int reviewId)
        {
            //check if exist
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            // get review
            var review = _reviewRepository.GetReview(reviewId);

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using review dto to return only the needed value
            var reviewDto = new ReviewDto()
            {
                Id = review.Id,
                Headline = review.Headline,
                ReviewText = review.ReviewText,
                Rating = review.Rating
            };


            return Ok(reviewDto);
        }




        // api/reviews/books/bookId
        [HttpGet("books/{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviewsOfABook(int bookId)
        {
            //check if exist
            if (!_bookRepository.BookExists(bookId))
                return NotFound();

            // get reviews
            var reviews = _reviewRepository.GetReviewsOfABook(bookId);

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using reviews dto to return only the needed value
            var reviewsDto = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewsDto.Add(new ReviewDto
                {
                    Id = review.Id,
                    Headline = review.Headline,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                });
            }

            return Ok(reviewsDto);
        }


        // api/reviews/reviewId/book
        [HttpGet("{reviewId}/book")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        public IActionResult GetbookOfAReview(int reviewId)
        {
            //check if exist
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            // get book
            var book = _reviewRepository.GetbookOfAReview(reviewId);

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using book dto to return only the needed value
            var bookDto = new BookDto()
            {
                Id = book.Id,
                Isbn= book.Isbn,
                Title = book.Title,
                DatePublished = book.DatePublished
            };


            return Ok(bookDto);
        }

    }
}
