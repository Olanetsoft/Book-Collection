﻿using BookCollectionAPI.Dtos;
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

    public class ReviewerController : Controller
    {
        private IReviewerRepository _reviewerRepository;
        private IReviewRepository _reviewRepository;

        // Constructor
        public ReviewerController(IReviewerRepository reviewerRepository, IReviewRepository reviewRepository)
        {
            _reviewerRepository = reviewerRepository;
            _reviewRepository = reviewRepository;
        }
        // 639226325888


        // api/reviewer
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public IActionResult GetReviewers()
        {
            // get reviewers
            var reviewers = _reviewerRepository.GetReviewers().ToList();

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using reviewers dto to return only the needed value
            var reviewerDto = new List<ReviewerDto>();
            foreach (var reviewer in reviewers)
            {
                reviewerDto.Add(new ReviewerDto
                {
                    Id = reviewer.Id,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }

            return Ok(reviewerDto);
        }




        // api/reviewer/reviewId
        [HttpGet("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ReviewerDto))]
        public IActionResult GetReviewer(int reviewerId)
        {
            //check if exist
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            // get reviewer
            var reviewer = _reviewerRepository.GetReviewer(reviewerId);

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using reviewer dto to return only the needed value
            var reviewerDto = new ReviewerDto()
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };


            return Ok(reviewerDto);
        }




        // api/reviewer/reviewId/reviews
        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            //check if exist
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            // get reviews
            var reviews = _reviewerRepository.GetReviewsByReviewer(reviewerId);

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using reviews dto to return only the needed value
            var reviewDto = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewDto.Add(new ReviewDto
                {
                    Id = review.Id,
                    Headline = review.Headline,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                });
            }

            return Ok(reviewDto);
        }

        // api/reviewer/reviewId/reviewer
        [HttpGet("{reviewId}/reviewer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        public IActionResult GetReviewersOfAReview(int reviewId)
        {
            //check if exist
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            // get reviewer
            var reviewer = _reviewerRepository.GetReviewerOfAReview(reviewId);

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using reviewer dto to return only the needed value
            var reviewerDto = new ReviewerDto()
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };


            return Ok(reviewerDto);
        }
    }
}
